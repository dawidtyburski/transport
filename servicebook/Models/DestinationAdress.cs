namespace transport.Models
{
    public class DestinationAdress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
