using FinanceTracker.Api.Common;

namespace FinanceTracker.Api.Models
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionCategory Category { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
    }
}
