namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumsService : IAlbumsService
    {
        private readonly RunesDbContext context;

        public AlbumsService(RunesDbContext context)
        {
            this.context = context;
        }

        public void Create(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0.0m
            };

            this.context.Albums.Add(album);
            this.context.SaveChanges();
        }

        public Album GetAlbumById(string id)
        {
            var album = this.context.Albums.FirstOrDefault(a => a.Id==id);

            return album;
        }

        public Album GetAlbumWithTracks(string albumId)
        {
            var albumWithTracks = this.context.Albums.Include(t => t.Tracks)
                .Where(a => a.Id == albumId).FirstOrDefault();

            return albumWithTracks;
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            var allAlbums = this.context.Albums.
                Include(t => t.Tracks)
                .ToList();

            return allAlbums;
        }
    }
}
