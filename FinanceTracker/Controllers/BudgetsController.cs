using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        public async Task<IActionResult> SetBudget([FromBody] SetBudgetDto dto)
        {
            var budget = await _budgetService.SetBudgetAsync(dto);
            return Ok(budget);
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await _budgetService.GetAllBudgetsAsync();
            return Ok(budgets);
        }
    }
}
