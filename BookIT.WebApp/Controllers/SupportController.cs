using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookIT.WebApp.Controllers
{
    [Authorize(Roles = "support")]
    [Route ("/support")]
    public class SupportController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ISupportService _supportService;

        public SupportController(IBookingService bookingService, ISupportService supportService)
        {
            _bookingService = bookingService;
            _supportService = supportService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }
        [HttpGet("chats")]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _supportService.GetActiveSupportChatsAsync();
            return Json(chats);
        }

        [HttpGet("messages/{userId}")]
        public async Task<IActionResult> GetMessages(string userId)
        {
            var messages = await _supportService.GetMessagesWithUserAsync(userId);
            return Json(messages);
        }
    }
}
