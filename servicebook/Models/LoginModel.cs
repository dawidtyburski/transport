using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }

    }
}
