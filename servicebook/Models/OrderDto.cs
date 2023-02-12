using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class OrderDto
    {
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        [Display(Name = "Pallets")]
        public float PalletPlace { get; set; }       
        public int Price { get; set; }


        public string PickupCity { get; set; }
        public string PickupPostCode { get; set; }
        [Display(Name = "From")]
        public string PickupCountry { get; set; }

        public string DestCity { get; set; }
        public string DestPostCode { get; set; }
        [Display(Name = "To")]
        public string DestCountry { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }



    }
}
