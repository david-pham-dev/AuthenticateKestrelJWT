using System.ComponentModel.DataAnnotations;

namespace AuthenticateAppBackEnd.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Your Password Must Have AT LEAST 6 Characters")]
        public string? Password { get; set; }
    }
}
