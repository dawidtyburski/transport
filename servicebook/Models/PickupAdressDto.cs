namespace transport.Models
{
    public class PickupAdressDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public virtual List<OrderDto> Orders { get; set; }
    }
}
