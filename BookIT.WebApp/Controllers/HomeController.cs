using BookIT.WebApp.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("support"))
            {
                return RedirectToAction("Support", "Dashboard");
            }
            else
            {
                return View();
            }
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
