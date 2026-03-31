using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public class BudgetRepository : JsonRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository() : base("budgets.json")
        {
        }

        public async Task<Budget?> GetBudgetAsync(Guid userId, TransactionCategory category, int month, int year)
        {
            var budgets = await GetAllAsync();
            return budgets.FirstOrDefault(b => b.UserId == userId && b.Category == category && b.Month == month && b.Year == year);
        }
    }
}
