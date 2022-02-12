using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Attributes;
using Sunshine.Enums;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "admin")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
