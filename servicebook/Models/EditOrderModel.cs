using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class EditOrderModel
    {
        [Required]
        [Range(0, 9999)]
        [Display(Name ="New Price")]
        public int Price { get; set; }
    }
}
