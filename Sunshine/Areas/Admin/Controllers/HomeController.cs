using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Database;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
