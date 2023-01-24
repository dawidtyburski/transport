using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace transport.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<PrincipalAdress> PrincipalAdresses { get; set; }
        public virtual ICollection<InitialAdress> InitialAdresses { get; set; }
        public virtual ICollection<DestinationAdress> DestinationAdresses { get; set; }
    }
}
