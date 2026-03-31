using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        public UserRepository() : base("users.json")
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var users = await GetAllAsync();
            return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
