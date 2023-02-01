using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
