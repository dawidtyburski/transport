namespace transport.Models
{
    public class ChangeRoleModel
    {
        public string ChangeRoleId { get; set; }
        public string ActualRole { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }
    }
}
