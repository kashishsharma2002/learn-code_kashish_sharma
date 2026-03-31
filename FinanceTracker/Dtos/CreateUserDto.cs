using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Api.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;
    }
}
