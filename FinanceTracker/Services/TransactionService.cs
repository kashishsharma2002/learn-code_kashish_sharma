using FinanceTracker.Api.Adapters;
using FinanceTracker.Api.Dtos;
using FinanceTracker.Api.Exceptions;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories;

namespace FinanceTracker.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IBudgetRepository budgetRepository,
            INotificationService notificationService,
            IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task<(Transaction transaction, string? alertMessage)> AddTransactionAsync(AddTransactionDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new NotFoundException("User not found.");

            if (dto.Amount <= 0)
                throw new ValidationException("Amount must be greater than zero.");

            var transaction = new Transaction
            {
                UserId = dto.UserId,
                Amount = dto.Amount,
                Type = dto.Type,
                Category = dto.Category,
                Date = dto.Date,
                Description = dto.Description
            };

            await _transactionRepository.AddAsync(transaction);

            // Clean Code boundary integration: check for budget exceeded
            string? alertMessage = null;
            if (transaction.Type == TransactionType.Expense)
            {
                alertMessage = await CheckBudgetWarningAsync(user, transaction);
            }

            return (transaction, alertMessage);
        }

        private async Task<string?> CheckBudgetWarningAsync(User user, Transaction expense)
        {
            var budget = await _budgetRepository.GetBudgetAsync(expense.UserId, expense.Category, expense.Date.Month, expense.Date.Year);
            if (budget == null)
                return null;

            var monthStart = new DateTime(expense.Date.Year, expense.Date.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var transactions = await _transactionRepository.GetFilteredAsync(expense.UserId, monthStart, monthEnd, expense.Category);
            
            var totalSpent = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            if (totalSpent > budget.MonthlyLimit)
            {
                var message = $"BUDGET ALERT! Your {expense.Category} budget of {budget.MonthlyLimit} has been exceeded. Total spent: {totalSpent}.";
                await _notificationService.SendNotificationAsync(user.Email, message);
                return message;
            }

            return null;
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(Guid userId, DateTime? startDate, DateTime? endDate, TransactionCategory? category)
        {
            return await _transactionRepository.GetFilteredAsync(userId, startDate, endDate, category);
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new NotFoundException("Transaction not found.");

            await _transactionRepository.DeleteAsync(id);
        }
    }
}
