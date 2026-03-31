using System.ComponentModel.DataAnnotations;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Dtos
{
    public class SetBudgetDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Range(0, 6, ErrorMessage = "Category must be between 0 and 6.")]
        public TransactionCategory Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Monthly limit must be greater than zero.")]
        public decimal MonthlyLimit { get; set; }

        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }
    }
}
