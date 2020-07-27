using IdentityServer.Helpers;
using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class UserService : IUserService<User>
    {
        public User FirstOrDefault(Func<User, bool> filter = null)
        {
            return UserStore.Users.FirstOrDefault(filter);
        }

        public async Task<bool> PasswordSignInAsync(string userName, string password)
        {
            var user = UserStore.Users.Single(x => x.Username == userName);

            return await Task.FromResult(user.Password == password);
        }
    }
}
