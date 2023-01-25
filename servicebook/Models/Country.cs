namespace transport.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public List<InitialAdress> InitialAdresses { get; set; }
        public List<DestinationAdress> DestinationAdresses { get; set; }
        public List<PrincipalAdress> PrincipalAdresses { get; set; }
    }
}
