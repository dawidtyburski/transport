namespace transport.Models
{
    public class OrderQuery
    {
        public string from { get; set; }
        public string to { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public SortDirection sortDirection { get; set; }
    }
}
