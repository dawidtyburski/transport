using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
