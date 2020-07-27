using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Models
{
    public class User
    {
        public User()
        {
            Claims = new List<Claim>();
        }

        public string SubjectId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public int LastName { get; set; }

        public string Password { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public List<Claim> Claims { get; set; }
    }
}