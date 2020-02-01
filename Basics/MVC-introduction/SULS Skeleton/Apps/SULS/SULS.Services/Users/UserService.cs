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
        public User CreateUser(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User GetUserByParameters(string name, string password)
        {
            return this.context.Users.FirstOrDefault(user => user.Username == name
            && user.Password == password);
        }
    }
}
