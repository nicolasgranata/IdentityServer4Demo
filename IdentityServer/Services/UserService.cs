using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class UserService : IUserService<User>
    {
        public User FirstOrDefault(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PasswordSignInAsync(string userName, string password)
        {
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
