using Microsoft.AspNetCore.Authentication;

namespace transport.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; } 
        public float PalletPlace { get; set; }       
        public bool Directly { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int Price { get; set; }

        public virtual InitialAdress InitialAdress { get; set; }

        public virtual DestinationAdress DestinationAdress { get; set; }

        public virtual Principal Principal { get; set; }
    }
}
