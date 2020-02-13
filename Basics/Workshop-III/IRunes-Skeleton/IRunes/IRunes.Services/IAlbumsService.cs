namespace IRunes.Services
{
    using IRunes.Models;
    using System.Collections.Generic;

    public interface IAlbumsService
    {
        IEnumerable<Album> GetAllAlbums();

        void Create(string name, string cover);

        Album GetAlbumWithTracks(string albumId);

        Album GetAlbumById(string id);
    }
}
