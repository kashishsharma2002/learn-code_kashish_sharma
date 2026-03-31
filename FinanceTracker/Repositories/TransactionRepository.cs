using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public class TransactionRepository : JsonRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository() : base("transactions.json")
        {
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
        {
            var transactions = await GetAllAsync();
            return transactions.Where(t => t.UserId == userId);
        }

        public async Task<IEnumerable<Transaction>> GetFilteredAsync(Guid userId, DateTime? startDate, DateTime? endDate, TransactionCategory? category)
        {
            var transactions = await GetByUserIdAsync(userId);

            if (startDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                transactions = transactions.Where(t => t.Date < endDate.Value);
            }
            if (category.HasValue)
            {
                transactions = transactions.Where(t => t.Category == category.Value);
            }

            return transactions;
        }
    }
}
