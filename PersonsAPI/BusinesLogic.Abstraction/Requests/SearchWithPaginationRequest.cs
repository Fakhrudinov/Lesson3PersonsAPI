namespace BusinesLogic.Abstraction.Requests
{
    public class SearchWithPaginationRequest
    {
        public string SearchTerm { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
