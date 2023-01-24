namespace transport.Models
{
    public class Principal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }

        public virtual PrincipalAdress PrincipalAdress { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}
