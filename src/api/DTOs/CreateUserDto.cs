using System.ComponentModel.DataAnnotations;

namespace ERPBridge.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateUserDto
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}