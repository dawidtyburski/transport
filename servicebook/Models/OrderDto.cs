using Microsoft.AspNetCore.Authentication;

namespace transport.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; } 
        public float PalletPlace { get; set; }       
        public int Price { get; set; }

        public string PickupCity { get; set; }
        public string PickupPostCode { get; set; }
        public string PickupCountry { get; set; }

        public string DestCity { get; set; }
        public string DestPostCode { get; set; }
        public string DestCountry { get; set; }



    }
}
