using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class CreateOrderDto
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string Description { get; set; }
        [Required]
        [Range(0, 30000)]
        public float Weight { get; set; }
        [Required]
        [Range(0, 33)]
        public float PalletPlace { get; set; }
        [Required]
        [Range(0, 9999)]
        public int Price { get; set; }
        [Required]
        [MaxLength(20)]
        public string PickupCity { get; set; }
        [Required]
        [MaxLength(8)]
        public string PickupPostCode { get; set; }
        [Required]
        [MaxLength(20)]
        public string PickupCountry { get; set; }
        [Required]
        [MaxLength(20)]
        public string DestCity { get; set; }
        [Required]
        [MaxLength(8)]
        public string DestPostCode { get; set; }
        [Required]
        [MaxLength(20)]
        public string DestCountry { get; set; }
    }
}
