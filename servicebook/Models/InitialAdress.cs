namespace transport.Models
{
    public class InitialAdress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
