using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Exceptions;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;

namespace FinanceTracker.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionRepository _transactionRepository;

        public ReportService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<MonthlySummaryDto> GetMonthlySummaryAsync(Guid userId, int month, int year)
        {
            // Validate month and year
            if (month < 1 || month > 12)
                throw new ValidationException($"Invalid month. Month must be between 1 and 12, received: {month}");
            
            if (year < 1900 || year > 2100)
                throw new ValidationException($"Invalid year. Year must be between 1900 and 2100, received: {year}");

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var transactions = await _transactionRepository.GetFilteredAsync(userId, startDate, endDate, null);

            var totalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            var savings = totalIncome - totalExpense;

            return new MonthlySummaryDto
            {
                Month = month,
                Year = year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Savings = savings
            };
        }
    }
}
