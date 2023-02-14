namespace transport.Models
{
    public class AdminPanelModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int Counter { get; set; }
        public bool isBlocked { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
