using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
