namespace transport.Models
{
    public class DestinationAdress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
