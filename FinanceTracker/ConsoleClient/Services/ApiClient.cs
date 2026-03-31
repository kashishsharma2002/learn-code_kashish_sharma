using System.Text;
using System.Text.Json;
using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Services;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IConsoleService _consoleService;

    public ApiClient(string baseUrl, IConsoleService consoleService)
    {
        _baseUrl = baseUrl;
        _consoleService = consoleService;
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<UserResponse?> CreateUserAsync(string name, string email)
    {
        return await PostAsync<UserResponse>("/users", new { name, email });
    }

    public async Task<UserResponse?> GetUserAsync(Guid userId)
    {
        return await GetAsync<UserResponse>($"/users/{userId}");
    }

    public async Task<TransactionResponse?> AddTransactionAsync(AddTransactionRequest request)
    {
        return await PostAsync<TransactionResponse>("/transactions", request);
    }

    public async Task<List<TransactionResponse>?> GetTransactionsAsync(TransactionsQuery query)
    {
        var queryParams = BuildQueryParams(query);
        return await GetAsync<List<TransactionResponse>>($"/transactions?{queryParams}");
    }

    public async Task<bool> DeleteTransactionAsync(Guid transactionId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(BuildUrl($"/transactions/{transactionId}"));
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _consoleService.WriteError($"Failed to delete transaction: {ex.Message}");
        }
        return false;
    }

    public async Task<BudgetResponse?> SetBudgetAsync(SetBudgetRequest request)
    {
        return await PostAsync<BudgetResponse>("/budgets", request);
    }

    public async Task<List<BudgetResponse>?> GetBudgetsAsync()
    {
        return await GetAsync<List<BudgetResponse>>("/budgets");
    }

    public async Task<MonthlySummaryResponse?> GetMonthlySummaryAsync(Guid userId, int month, int year)
    {
        return await GetAsync<MonthlySummaryResponse>($"/reports/summary?userId={userId}&month={month}&year={year}");
    }

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(BuildUrl(endpoint));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, _jsonOptions);
            }
        }
        catch (Exception ex)
        {
            _consoleService.WriteError($"API request failed: {ex.Message}");
        }
        return default;
    }

    private async Task<T?> PostAsync<T>(string endpoint, object request)
    {
        try
        {
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(BuildUrl(endpoint), content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, _jsonOptions);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _consoleService.WriteError($"Request failed: {response.StatusCode} - {errorContent}");
            }
        }
        catch (Exception ex)
        {
            _consoleService.WriteError($"API request failed: {ex.Message}");
        }
        return default;
    }

    private string BuildUrl(string endpoint) => $"{_baseUrl}{endpoint}";

    private string BuildQueryParams(TransactionsQuery query)
    {
        var queryParams = new List<string> { $"userId={query.UserId}" };
        if (query.StartDate.HasValue) queryParams.Add($"startDate={query.StartDate:yyyy-MM-dd}");
        if (query.EndDate.HasValue) queryParams.Add($"endDate={query.EndDate:yyyy-MM-dd}");
        if (query.Category.HasValue) queryParams.Add($"category={query.Category}");
        return string.Join("&", queryParams);
    }
}
