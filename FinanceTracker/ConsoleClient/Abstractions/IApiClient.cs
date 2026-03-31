using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Abstractions;

public interface IApiClient
{
    Task<UserResponse?> CreateUserAsync(string name, string email);
    Task<UserResponse?> GetUserAsync(Guid userId);
    Task<TransactionResponse?> AddTransactionAsync(AddTransactionRequest request);
    Task<List<TransactionResponse>?> GetTransactionsAsync(TransactionsQuery query);
    Task<bool> DeleteTransactionAsync(Guid transactionId);
    Task<BudgetResponse?> SetBudgetAsync(SetBudgetRequest request);
    Task<List<BudgetResponse>?> GetBudgetsAsync();
    Task<MonthlySummaryResponse?> GetMonthlySummaryAsync(Guid userId, int month, int year);
}
