using SULS.Data;
using SULS.Models;
using System.Linq;

namespace SULS.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SULSContext context;

        public UserService(SULSContext context)
        {
            this.context = context;
        }

        public void CreateUser(User user)
        {
            var userToCheck = this.context.Users.Any(u => u.Username == user.Username || u.Password == user.Password);

            if (userToCheck)
            {
                return;
            }

            this.context.Users.Add(user);
            this.context.SaveChanges();

        }

        public User GetUserByParameters(string name, string password)
        {
            return this.context.Users.FirstOrDefault(user => user.Username == name
            && user.Password == password);
        }

        public string GetUsernameById(string id)
        {
            return this.context.Users.Where(u => u.Id == id).Select(u => u.Username).FirstOrDefault();
        }

    }
}
