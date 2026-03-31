using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Transaction>> GetFilteredAsync(Guid userId, DateTime? startDate, DateTime? endDate, TransactionCategory? category);
    }
}
