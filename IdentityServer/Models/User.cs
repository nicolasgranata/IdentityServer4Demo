namespace IdentityServer.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public int LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}