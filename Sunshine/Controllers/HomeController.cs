using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Database;

namespace Sunshine.Controllers
{
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
