namespace BookIT.WebApp.ViewModels.Catalog.Room
{
    public class PaginatedRoomViewModel
    {
        public List<RoomViewModel> Rooms { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SortBy { get; set; }
    }

}
