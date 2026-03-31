using System.Text;
using System.Text.Json;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Services
{
    public class FinanceTrackerApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public FinanceTrackerApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        }

        public async Task<UserResponse?> CreateUserAsync(string name, string email)
        {
            try
            {
                var request = new { name, email };
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/users", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
            }
            return null;
        }

        public async Task<UserResponse?> GetUserAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<UserResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"User not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user: {ex.Message}");
            }
            return null;
        }
        public async Task<TransactionResponse?> AddTransactionAsync(Guid userId, int type, decimal amount, int category, DateTime date, string description)
        {
            try
            {
                var request = new { userId, type, amount, category, date, description };
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/transactions", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TransactionResponse>(json, _jsonOptions);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding transaction: {ex.Message}");
            }
            return null;
        }

        public async Task<List<TransactionResponse>?> GetTransactionsAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null, int? category = null)
        {
            try
            {
                var queryParams = new List<string> { $"userId={userId}" };
                if (startDate.HasValue) queryParams.Add($"startDate={startDate:yyyy-MM-dd}");
                if (endDate.HasValue) queryParams.Add($"endDate={endDate:yyyy-MM-dd}");
                if (category.HasValue) queryParams.Add($"category={category}");

                var url = $"{_baseUrl}/transactions?" + string.Join("&", queryParams);
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TransactionResponse>>(json, _jsonOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transactions: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> DeleteTransactionAsync(Guid transactionId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/transactions/{transactionId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting transaction: {ex.Message}");
            }
            return false;
        }

        public async Task<BudgetResponse?> SetBudgetAsync(Guid userId, int category, decimal monthlyLimit, int month, int year)
        {
            try
            {
                var request = new { userId, category, monthlyLimit, month, year };
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/budgets", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BudgetResponse>(json, _jsonOptions);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting budget: {ex.Message}");
            }
            return null;
        }

        public async Task<List<BudgetResponse>?> GetBudgetsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/budgets");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<BudgetResponse>>(json, _jsonOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving budgets: {ex.Message}");
            }
            return null;
        }

        public async Task<MonthlySummaryResponse?> GetMonthlySummaryAsync(Guid userId, int month, int year)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/reports/summary?userId={userId}&month={month}&year={year}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<MonthlySummaryResponse>(json, _jsonOptions);
                }
                else
                {
                    Console.WriteLine($"No summary available");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving summary: {ex.Message}");
            }
            return null;
        }
    }
}
