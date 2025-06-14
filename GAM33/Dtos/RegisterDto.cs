using System.ComponentModel.DataAnnotations;

namespace GAM33.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
         ErrorMessage = "Password must be at least 8 characters long, contain uppercase, lowercase, number, and special character.")]
        public required string Password { get; set; }

        [Required]
        public required string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


    }
}
