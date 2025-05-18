using System.ComponentModel.DataAnnotations;
using TSUApplicationApi.Attributes;

namespace TSUApplicationApi.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [PasswordComplexity]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
