using Microsoft.EntityFrameworkCore;
using Sunshine.Enums;
using Sunshine.Models;
using Sunshine.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Sunshine.Reporitories
{
    public class AuthRepository
    {
        private readonly AppDatabaseContext _ctx;
        public AuthRepository(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }

        public bool IsEmailTaken(AuthLoginViewModel authLoginViewModel)
        {
            if (_ctx.Users.Any(u => u.Email == authLoginViewModel.Email))
                return true;
            return false;
        }
        
        public async Task<User> IsValidAuthData(AuthLoginViewModel authLoginViewModel)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == authLoginViewModel.Email && u.Password == authLoginViewModel.Password);
        }
        
        public async Task<User> Resigter(AuthRegisterViewModel authRegisterViewModel)
        {
            User newUser = new User();
            newUser.Email = authRegisterViewModel.Email;
            newUser.Password = authRegisterViewModel.Password;
            newUser.FirstName = authRegisterViewModel.FirstName;
            newUser.LastName = authRegisterViewModel.LastName;
            newUser.Role = Role.user.ToString();
            _ctx.Users.Add(newUser);
            await _ctx.SaveChangesAsync();
            return newUser;
        }
    }
}
