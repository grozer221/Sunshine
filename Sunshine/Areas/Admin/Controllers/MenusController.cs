using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Areas.Admin.Reporitories;
using Sunshine.Areas.Admin.ViewModels.Menus;
using Sunshine.Attributes;
using Sunshine.Enums;
using Sunshine.Models;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class MenusController : Controller
    {
        private readonly MenusRepository _menusRep;
        private readonly SubMenusRepository _subMenusRep;

        public MenusController(MenusRepository menusRep, SubMenusRepository subMenusRep)
        {
            _menusRep = menusRep;
            _subMenusRep = subMenusRep;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            var menus = await _menusRep.Get();
            return View(menus);
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            MenusDetailsViewModel menusIndexViewModel = new MenusDetailsViewModel();
            menusIndexViewModel.Menu = await _menusRep.GetById(id);
            if (menusIndexViewModel.Menu == null)
                return NotFound();
            menusIndexViewModel.SubMenus = await _subMenusRep.GetByMenuId(menusIndexViewModel.Menu.Id);
            return View(menusIndexViewModel);
        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _menusRep.Create(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            MenusEditViewModel menusEditViewModel = new MenusEditViewModel();
            menusEditViewModel.Menu = await _menusRep.GetById(id);
            if (menusEditViewModel.Menu == null)
            {
                return NotFound();
            }
            return View(menusEditViewModel);
        }

        // POST: Admin/Menus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _menusRep.Update(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Menu menu = await _menusRep.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menusRep.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Menus/Resorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            await _menusRep.Reorder(id);
            return Ok();
        }
    }
}
