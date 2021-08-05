namespace PersonsAPI.Requests
{
    public class SearchWithPagination
    {
        public string SearchTerm { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
