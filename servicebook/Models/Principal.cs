namespace transport.Models
{
    public class Principal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string AccessCode { get; set; }

        public int PrincipalAdressId { get; set; }
        public virtual PrincipalAdress PrincipalAdress { get; set; }
        
        public virtual List<Order> Orders { get; set; }
    }
}
