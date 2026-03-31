using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<Budget?> GetBudgetAsync(Guid userId, TransactionCategory category, int month, int year);
    }
}
