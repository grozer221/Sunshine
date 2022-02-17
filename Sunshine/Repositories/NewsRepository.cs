using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using System.Collections.Generic;
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
        public async Task<IEnumerable<New>> Get()
        {
            return await _ctx.News.ToListAsync();
        }

        public async Task<New> GetById(int? id)
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
