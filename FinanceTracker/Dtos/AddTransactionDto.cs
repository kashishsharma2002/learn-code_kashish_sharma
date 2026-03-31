using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Dtos
{
    public class AddTransactionDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Transaction type is required.")]
        [Range(0, 1, ErrorMessage = "Transaction type must be 0 (Income) or 1 (Expense).")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Range(0, 6, ErrorMessage = "Category must be between 0 and 6.")]
        public TransactionCategory Category { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
