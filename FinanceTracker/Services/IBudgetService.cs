using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Services;

public interface IBudgetService
{
    Task<Budget> SetBudgetAsync(SetBudgetDto dto);
    Task<IEnumerable<Budget>> GetAllBudgetsAsync();
}
