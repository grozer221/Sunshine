using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunshine.Areas.Admin.ViewModels;
using Sunshine.Models;
using Sunshine.Repositories;

namespace Sunshine.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class SubMenusController : Controller
    {
        private readonly MenusRepository _menusRep;
        private readonly SubMenusRepository _subMenusRep;

        public SubMenusController(MenusRepository menusRep, SubMenusRepository subMenusRep)
        {
            _menusRep = menusRep;
            _subMenusRep = subMenusRep;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            IEnumerable<SubMenu> menus = await _subMenusRep.GetIncludedMenu();
            return View(menus);
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            SubMenu subMenu = await _subMenusRep.GetById(id);
            return View(subMenu);
        }

        // GET: Admin/Menus/Create
        public async Task<IActionResult> Create()
        {
            SubMenusCreateViewModel subMenusCreateViewModel = new SubMenusCreateViewModel();
            subMenusCreateViewModel.Menus = await _menusRep.Get();
            return View(subMenusCreateViewModel);
        }

        // POST: Admin/Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Text,MenuId")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                await _subMenusRep.Create(subMenu);
                return RedirectToAction(nameof(Index));
            }
            return View(subMenu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            SubMenusEditViewModel subMenusEditViewModel = new SubMenusEditViewModel();
            subMenusEditViewModel.SubMenu = await _subMenusRep.GetById(id);
            if (subMenusEditViewModel.SubMenu == null)
                return NotFound();
            subMenusEditViewModel.SelectedMenu = await _menusRep.GetMenuBySubMenuId(subMenusEditViewModel.SubMenu.Id);
            subMenusEditViewModel.Menus = await _menusRep.Get();
            return View(subMenusEditViewModel);
        }

        // POST: Admin/Menus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Text,MenuId")] SubMenu subMenu)
        {
            if (id != subMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _subMenusRep.Update(subMenu);
                return RedirectToAction(nameof(Index));
            }
            return View(subMenu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            SubMenu subMenu = await _subMenusRep.GetById(id);
            if (subMenu == null)
            {
                return NotFound();
            }
            return View(subMenu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subMenusRep.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Menus/Resorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            await _subMenusRep.Reorder(id);
            return Ok();
        }
    }
}
