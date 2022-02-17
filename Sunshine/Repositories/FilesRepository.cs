using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Repositories
{
    public class FilesRepository
    {
        private readonly AppDatabaseContext _ctx;
        public FilesRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<File>> Get()
        {
            return await _ctx.Files.OrderByDescending(m => m.CreatedAt).ToListAsync();
        }

        public async Task<File> GetById(int id)
        {
            return await _ctx.Files.FindAsync(id);
        }

        public async Task<File> Create(File file)
        {
            _ctx.Files.Add(file);
            await _ctx.SaveChangesAsync();
            return file;
        }

        public async Task<File> Update(File file)
        {
            _ctx.Files.Update(file);
            await _ctx.SaveChangesAsync();
            return file;
        }

        public async Task Delete(int id)
        {
            File file = await GetById(id);
            _ctx.Files.Remove(file);
            await _ctx.SaveChangesAsync();
        }
    }
}
