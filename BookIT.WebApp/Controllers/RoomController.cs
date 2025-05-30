using BookIT.WebApp.ViewModels.Catalog.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookIT.WebApp.Controllers
{
    public class RoomController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public RoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync($"room/api/room/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomViewModel>(json);

            return View(room);
        }
    }
}
