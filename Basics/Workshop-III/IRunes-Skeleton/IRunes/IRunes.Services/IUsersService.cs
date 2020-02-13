namespace IRunes.Services
{
    public interface IUsersService
    {
        string GetUsername(string id);

        bool UsernameExists(string username);

        bool EmailExists(string email);

        string GetUserId(string username, string password);

        void Register(string username, string password, string email);
    }
}
