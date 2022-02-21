using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Repositories
{
    public class MenusRepository
    {
        private readonly AppDatabaseContext _ctx;
        public MenusRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Menu>> Get()
        {
            return await _ctx.Menus.OrderBy(m => m.Sorting).ToListAsync();
        }
        
        public async Task<IEnumerable<Menu>> GetIncludedSubMenus()
        {
            IEnumerable<Menu> menus = await _ctx.Menus.Include(m => m.SubMenus).OrderBy(m => m.Sorting).ToListAsync();
            foreach (var menu in menus)
                menu.SubMenus = menu.SubMenus.OrderBy(s => s.Sorting);
            return menus;
        }
        
        public async Task<Menu> GetById(int id)
        {
            return await _ctx.Menus.FindAsync(id);
        }

        public async Task<Menu> Create(Menu menu)
        {
            _ctx.Menus.Add(menu);
            await _ctx.SaveChangesAsync();
            return menu;
        }
        
        public async Task<Menu> Update(Menu menu)
        {
            _ctx.Menus.Update(menu);
            await _ctx.SaveChangesAsync();
            return menu;
        }
        
        public async Task Delete(int id)
        {
            Menu menu = await GetById(id);
            _ctx.Menus.Remove(menu);
            await _ctx.SaveChangesAsync();
        }

        public async Task Reorder(int[] ids)
        {
            int count = 0;
            foreach(int id in ids)
            {
                Menu menu = await _ctx.Menus.FindAsync(id);
                menu.Sorting = count;
                _ctx.Menus.Update(menu);
                await _ctx.SaveChangesAsync();
                count++;
            }
        }

        public async Task<Menu> GetMenuBySubMenuId(int subMenuId)
        {
            var subMenu = await _ctx.SubMenus.Include(s => s.Menu).FirstOrDefaultAsync(s => s.Id == subMenuId);
            if (subMenu == null)
                throw new Exception("Sub menu does not exists");
            return subMenu.Menu;
        }
    }
}
