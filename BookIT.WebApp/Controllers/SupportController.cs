using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookIT.WebApp.Controllers
{
    [Route("api/{controller}")]
    public class SupportController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IBookingService _bookingService;

        public SupportController(IHttpClientFactory clientFactory, IBookingService bookingService)
        {
            _clientFactory = clientFactory;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var client = _clientFactory.CreateClient("ApiGateway");

            var response = await client.GetAsync("booking/api/booking");

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Ошибка при получении данных: {response.StatusCode} — {errorText}");
            }
            var bookings = await _bookingService.GetUserBookingsAsync();

            var content = await response.Content.ReadFromJsonAsync<List<BookingViewModel>>();
            return View(content);
        }

    }
}
