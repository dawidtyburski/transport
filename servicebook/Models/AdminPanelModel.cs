using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class AdminPanelModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Created Orders")]
        public int Counter { get; set; }
        public bool isBlocked { get; set; }
        public string Role { get; set; } 
    }

}
