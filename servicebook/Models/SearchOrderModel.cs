namespace transport.Models
{
    public class SearchOrderModel
    {
        public string From { get; set; }
        public string To { get; set; }

        public enum Countries
        {
            Austria,
            Belgium,
            Bulgaria,
            Croatia,
            Cyprus,
            Czech,
            Denmark,
            Estonia,
            Finland,
            France,
            Germany,
            Greece,
            Hungary,
            Ireland,
            Italy,
            Latvia,
            Lithuania,
            Luxembourg,
            Malta,
            Netherlands,
            Poland,
            Portugal,
            Romania,
            Slovakia,
            Slovenia,
            Spain,
            Sweden
        }

        public List<string> countries { get; set; }

        public SearchOrderModel()
        {
            this.countries = new List<string>(Enum.GetNames(typeof(Countries)).ToList());
        }

    }
}
