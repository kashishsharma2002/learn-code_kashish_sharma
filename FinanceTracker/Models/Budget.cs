using FinanceTracker.Api.Common;

namespace FinanceTracker.Api.Models
{
    public class Budget : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public TransactionCategory Category { get; set; }
        public decimal MonthlyLimit { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
