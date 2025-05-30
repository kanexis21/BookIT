namespace BookIT.WebApp.ViewModels.Catalog.Resource
{
    public class PaginatedResourceViewModel
    {
        public List<ResourceViewModel> Resources { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortBy { get; set; }
    }

}
