using BookIT.WebApp.ViewModels.Catalog;
using BookIT.WebApp.ViewModels.Catalog.Resource;
using BookIT.WebApp.ViewModels.Catalog.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Authorize]
public class CatalogController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(string tab = "resources", string sortBy = "", int page = 1, int pageSize = 10)
    {
        var client = _httpClientFactory.CreateClient("ApiGateway");
        var model = new CatalogPageViewModel { ActiveTab = tab };

        if (tab == "resources")
        {
            var resResponse = await client.GetAsync("resource/api/resource");
            if (!resResponse.IsSuccessStatusCode)
                return View(model);

            var json = await resResponse.Content.ReadAsStringAsync();
            var allResources = JsonConvert.DeserializeObject<List<ResourceViewModel>>(json);

            allResources = sortBy switch
            {
                "name" => allResources.OrderBy(r => r.Name).ToList(),
                "status" => allResources.OrderBy(r => r.Status).ToList(),
                _ => allResources
            };

            var paged = new PaginatedResourceViewModel
            {
                Resources = allResources.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)allResources.Count / pageSize),
                SortBy = sortBy
            };

            model.Resources = paged;
        }
        else if (tab == "rooms")
        {
            var roomResponse = await client.GetAsync("room/api/room");
            if (!roomResponse.IsSuccessStatusCode)
                return View(model);

            var json = await roomResponse.Content.ReadAsStringAsync();
            var allRooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(json);

            allRooms = sortBy switch
            {
                "name" => allRooms.OrderBy(r => r.Name).ToList(),
                _ => allRooms
            };

            var paged = new PaginatedRoomViewModel
            {
                Rooms = allRooms.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)allRooms.Count / pageSize),
                SortBy = sortBy
            };

            model.Rooms = paged;
        }

        return View(model);
    }


}
