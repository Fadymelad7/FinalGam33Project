using System.ComponentModel.DataAnnotations;

namespace GAM33.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

    }
}
