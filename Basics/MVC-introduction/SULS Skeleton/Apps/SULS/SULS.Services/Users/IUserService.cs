using SULS.Models;

namespace SULS.Services.Users
{
    public interface IUserService
    {
        User CreateUser(User user);

        User GetUserByParameters(string name, string password);
    }
}
