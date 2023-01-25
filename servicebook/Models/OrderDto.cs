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

        public string City1 { get; set; }
        public string City2 { get; set; }

        public string PostCode1 { get; set; }
        public string PostCode2 { get; set; }

        public string Country1 { get; set; }
        public string Country2 { get; set; }

        public string Principal { get; set; }
    }
}
