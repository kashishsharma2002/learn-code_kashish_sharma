using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Exceptions;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;

namespace FinanceTracker.Api.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUserRepository _userRepository;

    public BudgetService(IBudgetRepository budgetRepository, IUserRepository userRepository)
    {
        _budgetRepository = budgetRepository;
        _userRepository = userRepository;
    }

    public async Task<Budget> SetBudgetAsync(SetBudgetDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new NotFoundException("User not found.");

        if (dto.MonthlyLimit <= 0)
            throw new ValidationException("Budget limit must be greater than zero.");

        var existingBudget = await _budgetRepository.GetBudgetAsync(dto.UserId, dto.Category, dto.Month, dto.Year);

        if (existingBudget != null)
        {
            existingBudget.MonthlyLimit = dto.MonthlyLimit;
            await _budgetRepository.UpdateAsync(existingBudget);
            return existingBudget;
        }

        var budget = new Budget
        {
            UserId = dto.UserId,
            Category = dto.Category,
            MonthlyLimit = dto.MonthlyLimit,
            Month = dto.Month,
            Year = dto.Year
        };

        await _budgetRepository.AddAsync(budget);
        return budget;
    }

    public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
    {
        return await _budgetRepository.GetAllAsync();
    }
}
