using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

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


        public int PickupAdressId { get; set; }
        public virtual PickupAdress PickupAdress { get; set; }

        public int DestinationAdressId { get; set; }
        public virtual DestinationAdress DestinationAdress { get; set; }

    }
}
