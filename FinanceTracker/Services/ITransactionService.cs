using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Services
{
    public interface ITransactionService
    {
        Task<(Transaction transaction, string? alertMessage)> AddTransactionAsync(AddTransactionDto dto);
        Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate, TransactionCategory? category);
        Task DeleteTransactionAsync(Guid id);
    }
}
