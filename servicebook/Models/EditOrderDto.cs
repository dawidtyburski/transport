using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class EditOrderDto
    {
        [Required]
        [Range(0, 9999)]
        public int Price { get; set; }
    }
}
