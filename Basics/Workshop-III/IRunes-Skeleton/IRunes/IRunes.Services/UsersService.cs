namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly RunesDbContext context;

        public UsersService(RunesDbContext context)
        {
            this.context = context;
        }

        public bool EmailExists(string email)
        {
            return this.context.Users.Any(u => u.Email == email);
        }

        public bool UsernameExists(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public string GetUsername(string id)
        {
            var username = this.context.Users.Where(u => u.Id == id)
                .Select(n => n.Username)
                .FirstOrDefault();

            if (username == null)
            {
                return null;
            }

            return username;
        }

        public string GetUserId(string username, string password)
        {
            var hasPassword = this.Hash(password);
            var userId = this.context.Users.Where(u => u.Username == username || u.Email == username
            && u.Password == hasPassword)
                .Select(u => u.Id).FirstOrDefault();

            if (userId == null)
            {
                return null;
            }

            return userId;
        }

        public void Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.Hash(password)
            };

            this.context.Users.Add(user);

            this.context.SaveChanges();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }


    }
}
