using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Areas.Admin.Reporitories;
using Sunshine.ViewModels;

namespace Sunshine.Controllers
{
    public class HomeController : Controller
    {
        private readonly MenusRepository _menusRepository;
        private readonly NewsRepository _newsRepository;
        public HomeController(MenusRepository menusRepository, NewsRepository newsRepository)
        {
            _menusRepository = menusRepository;
            _newsRepository = newsRepository;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel
            {
                News = await _newsRepository.Get()
            };
            return View(homeIndexViewModel);
        }
    }
}
