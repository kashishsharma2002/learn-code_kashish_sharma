using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDto dto)
        {
            var (transaction, alertMessage) = await _transactionService.AddTransactionAsync(dto);
            var response = new TransactionResponseDto
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Amount = transaction.Amount,
                Type = (int)transaction.Type,
                Category = (int)transaction.Category,
                Date = transaction.Date,
                Description = transaction.Description,
                AlertMessage = alertMessage
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(
            [FromQuery] Guid userId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] TransactionCategory? category)
        {
            var transactions = await _transactionService.GetFilteredTransactionsAsync(userId, startDate, endDate, category);
            return Ok(transactions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}
