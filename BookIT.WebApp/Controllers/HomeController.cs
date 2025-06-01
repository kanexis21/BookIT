using BookIT.WebApp.Application.Services.Interfaces;
using BookIT.WebApp.ViewModels;
using BookIT.WebApp.ViewModels.Support;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace BookIT.WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISupportService _supportService;
        public HomeController(ISupportService supportService)
        {
            _supportService = supportService;
        }
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("support"))
            {
                return RedirectToAction("Dashboard", "Support");
            }
            else
            {
                return View();
            }
        }

        [HttpPost("messages/send")]
        public async Task<IActionResult> SendMessage([FromBody] SupportMessageViewModel message)
        {
            await _supportService.SendSupportMessageAsync(message);
            return Ok(message);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Logout", "Account")
            },
            "Cookies",
            "OpenIdConnect");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
