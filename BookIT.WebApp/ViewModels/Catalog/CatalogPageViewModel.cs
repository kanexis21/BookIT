using BookIT.WebApp.ViewModels.Catalog.Resource;
using BookIT.WebApp.ViewModels.Catalog.Room;

namespace BookIT.WebApp.ViewModels.Catalog
{
    public class CatalogPageViewModel
    {
        public string ActiveTab { get; set; }

        public PaginatedResourceViewModel? Resources { get; set; }
        public PaginatedRoomViewModel? Rooms { get; set; }
    }
}
