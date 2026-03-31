using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task<User> GetUserAsync(Guid id);
    }
}
