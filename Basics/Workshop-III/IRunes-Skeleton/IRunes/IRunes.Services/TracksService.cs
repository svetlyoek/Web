namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Models;
    using System.Linq;
    public class TracksService : ITracksService
    {
        private readonly RunesDbContext context;

        public TracksService(RunesDbContext context)
        {
            this.context = context;
        }

        public void Create(string name, decimal price, string link, string albumId)
        {
            var album = this.context.Albums.Find(albumId);

            var track = new Track
            {
                AlbumId = albumId,
                Name = name,
                Price = price,
                Link = link
            };

            this.context.Tracks.Add(track);

            var allTracksPrice = this.context.Tracks
                .Where(a => a.AlbumId == albumId)
                .Sum(p => p.Price) + price;

            album.Price = allTracksPrice * 0.87M;

            this.context.SaveChanges();
        }

        public Track GetById(string id)
        {
            return this.context.Tracks.FirstOrDefault(t=>t.Id==id);
        }
    }
}
