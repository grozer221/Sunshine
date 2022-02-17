using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Models;
using Sunshine.Repositories;

namespace Sunshine.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsRepository _newsRepository;
        private readonly MenusRepository _menusRepository;

        public NewsController(NewsRepository newsRepository, MenusRepository menusRepository)
        {
            _newsRepository = newsRepository;
            _menusRepository = menusRepository;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            IEnumerable<New> news = await _newsRepository.Get();
            return View(news);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            New neww = await _newsRepository.GetById(id);
            if (neww == null)
                return NotFound();
            return View(neww);
        }
    }
}
