namespace transport.Models
{
    public class Country
    {
        public string Id { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<PrincipalAdress> PrincipalAdresses { get; set; }
        public virtual ICollection<InitialAdress> InitialAdresses { get; set; }
        public virtual ICollection<DestinationAdress> DestinationAdresses { get; set; }
    }
}
