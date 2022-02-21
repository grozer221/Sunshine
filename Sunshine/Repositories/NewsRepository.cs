using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using Sunshine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Repositories
{
    public class NewsRepository
    {
        private readonly AppDatabaseContext _ctx;
        public NewsRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<NewsIndexViewModel> Get(int page = 1)
        {
            int take = 10;
            int skip = (page - 1) * take;
            return new NewsIndexViewModel
            {
                News = await _ctx.News.OrderByDescending(n => n.CreatedAt).Skip(skip).Take(take).ToListAsync(),
                PagesCount = (int)Math.Ceiling((decimal)_ctx.News.Count() / take),
                PageNumber = page,
            };
        }

        public async Task<New> GetById(int id)
        {
            return await _ctx.News.FindAsync(id);
        }

        public async Task<New> Create(New neww)
        {
            _ctx.News.Add(neww);
            await _ctx.SaveChangesAsync();
            return neww;
        }

        public async Task<New> Update(New neww)
        {
            _ctx.News.Update(neww);
            await _ctx.SaveChangesAsync();
            return neww;
        }

        public async Task Delete(int id)
        {
            New neww = await GetById(id);
            _ctx.News.Remove(neww);
            await _ctx.SaveChangesAsync();
        }
    }
}
