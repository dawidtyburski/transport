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
        public int Price { get; set; }

        public int InitialAdressId { get; set; }
        public virtual InitialAdress InitialAdress { get; set; }

        public int DestinationAdressId { get; set; }
        public virtual DestinationAdress DestinationAdress { get; set; }

        public int PrincipalId { get; set; }
        public virtual Principal Principal { get; set; }
    }
}
