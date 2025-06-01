using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookIT.WebApp.Controllers
{
    [Route("claims")]
    public class ClaimsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json(User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }

}
