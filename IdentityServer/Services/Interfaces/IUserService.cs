using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityServer.Services.Interfaces
{
    public interface IUserService<T> where T : class
    {
        /// <summary>
        /// Get First or Default
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>User</returns>
        T FirstOrDefault(Func<T, bool> filter = null);

        Task<bool> PasswordSignInAsync(string userName, string password);
    }
}
