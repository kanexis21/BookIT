using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BookIT.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserProfileService _profileService;
        private readonly IBookingService _bookingService;
        public ProfileController(IHttpClientFactory httpClientFactory, IUserProfileService profileService, IBookingService bookingService)
        {
            _httpClientFactory = httpClientFactory;
            _bookingService = bookingService;
            _profileService = profileService;
        }
        [HttpPost]
        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");

            var response = await client.DeleteAsync($"booking/api/booking/{bookingId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при удалении бронирования: {error}");

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var client = _httpClientFactory.CreateClient("ApiGateway");

            var updateModel = new
            {
                Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var content = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"auth/api/user/profile", content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Ошибка обновления профиля");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var profile = await _profileService.GetProfileAsync(userId);
            var bookings = await _bookingService.GetUserBookingsAsync(userId);

            var viewModel = new ProfilePageViewModel
            {
                Profile = profile,
                Bookings = bookings
            };

            return View(viewModel);
        }

    }

}
