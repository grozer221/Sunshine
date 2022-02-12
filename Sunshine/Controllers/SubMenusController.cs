using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunshine;
using Sunshine.Areas.Admin.Reporitories;
using Sunshine.Models;

namespace Sunshine.Controllers
{
    public class SubMenusController : Controller
    {
        private readonly SubMenusRepository _subMenusRepository;
        private readonly MenusRepository _menusRepository;

        public SubMenusController(MenusRepository menusRepository, SubMenusRepository subMenusRepository)
        {
            _menusRepository = menusRepository;
            _subMenusRepository = subMenusRepository;
        }


        // GET: SubMenus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Menus"] = await _menusRepository.GetIncludedSubMenus();

            SubMenu subMenu = await _subMenusRepository.GetById(id);
            if (subMenu == null)
            {
                return NotFound();
            }
            return View(subMenu);
        }
    }
}
