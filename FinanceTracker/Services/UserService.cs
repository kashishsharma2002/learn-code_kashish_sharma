using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Exceptions;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;

namespace FinanceTracker.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ValidationException("Email is required.");
        
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new ValidationException("User with this email already exists.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("User not found.");

        return user;
    }
}
