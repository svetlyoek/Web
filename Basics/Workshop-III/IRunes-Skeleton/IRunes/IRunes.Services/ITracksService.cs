using IRunes.Models;

namespace IRunes.Services
{
    public interface ITracksService
    {
        void Create(string name, decimal price, string link,string albumId);

        Track GetById(string id);
    }
}
