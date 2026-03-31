using FinanceTracker.Api.Common;

namespace FinanceTracker.Api.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
