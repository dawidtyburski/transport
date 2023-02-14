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

        public int Counter { get; set; }

        public bool isBlocked { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
