using Microsoft.EntityFrameworkCore;
using Sunshine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunshine.Repositories
{
    public class UsersRepository
    {
        private readonly AppDatabaseContext _ctx;
        public UsersRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _ctx.Users.ToListAsync();
        }
        
        public async Task<User> GetById(int id)
        {
            return await _ctx.Users.FindAsync(id);
        }
    }
}
