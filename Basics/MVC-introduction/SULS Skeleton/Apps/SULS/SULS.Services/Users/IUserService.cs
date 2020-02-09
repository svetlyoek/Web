using SULS.Models;

namespace SULS.Services.Users
{
    public interface IUserService
    {
        void CreateUser(User user);

        string GetUsernameById(string id);

        User GetUserByParameters(string name, string password);
    }
}
