using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Repositories
{
    public class SubMenusRepository
    {
        private readonly AppDatabaseContext _ctx;
        public SubMenusRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<SubMenu>> Get()
        {
            return await _ctx.SubMenus.OrderBy(s => s.Sorting).ToListAsync();
        }

        public async Task<IEnumerable<SubMenu>> GetIncludedMenu()
        {
            return await _ctx.SubMenus.Include(s => s.Menu).OrderBy(s => s.Sorting).ToListAsync();
        }

        public async Task<SubMenu> GetById(int id)
        {
            return await _ctx.SubMenus.FindAsync(id);
        }

        public async Task<SubMenu> Create(SubMenu subMenus)
        {
            _ctx.SubMenus.Add(subMenus);
            await _ctx.SaveChangesAsync();
            return subMenus;
        }

        public async Task<SubMenu> Update(SubMenu subMenu)
        {
            _ctx.SubMenus.Update(subMenu);
            await _ctx.SaveChangesAsync();
            return subMenu;
        }
        
        public async Task Delete(int id)
        {
            SubMenu submenu = await GetById(id);
            _ctx.SubMenus.Remove(submenu);
            await _ctx.SaveChangesAsync();
        }

        public async Task Reorder(int[] ids)
        {
            int count = 0;
            foreach(int id in ids)
            {
                SubMenu subMenu = await _ctx.SubMenus.FindAsync(id);
                subMenu.Sorting = count;
                _ctx.SubMenus.Update(subMenu);
                await _ctx.SaveChangesAsync();
                count++;
            }
        }

        public async Task<IEnumerable<SubMenu>> GetByMenuId(int id)
        {
            var menu = await _ctx.Menus.Include(m => m.SubMenus).FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
                throw new Exception("");
            return menu.SubMenus.OrderBy(s => s.Sorting);
        }
    }
}
