using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class CreateOrderModel
    {
        [Required]
        [MaxLength(15)]       
        public string Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string Description { get; set; }
        [Required]
        [Range(1, 30000)]
        public float Weight { get; set; }
        [Required]
        [Range(0, 33)]
        [Display(Name = "Pallets")]
        public float PalletPlace { get; set; }
        [Required]
        [Range(0, 9999)]
        public int Price { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "City")]
        public string PickupCity { get; set; }
        [Required]
        [MaxLength(8)]
        [Display(Name = "Post Code")]
        public string PickupPostCode { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Country")]
        public string PickupCountry { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "City")]
        public string DestCity { get; set; }
        [Required]
        [MaxLength(8)]
        [Display(Name = "Post Code")]
        public string DestPostCode { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Country")]
        public string DestCountry { get; set; }

        public enum Countries
        {
            Austria,
            Belgium,
            Bulgaria,
            Croatia,
            Cyprus,
            Czech,
            Denmark,
            Estonia,
            Finland,
            France,
            Germany,
            Greece,
            Hungary,
            Ireland,
            Italy,
            Latvia,
            Lithuania,
            Luxembourg,
            Malta,
            Netherlands,
            Poland,
            Portugal,
            Romania,
            Slovakia,
            Slovenia,
            Spain,
            Sweden
        }

        public List<string> countries { get; set; }

        public CreateOrderModel()
        {
            this.countries = new List<string>(Enum.GetNames(typeof(Countries)).ToList());
        }
    }
}
