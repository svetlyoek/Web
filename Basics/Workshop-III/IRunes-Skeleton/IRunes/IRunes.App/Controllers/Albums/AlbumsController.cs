namespace IRunes.App.Controllers.Albums
{
    using IRunes.App.ViewModels.Albums;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Linq;

    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumsCreateInputViewModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return this.Redirect("/Albums/Create");
            }

            this.albumsService.Create(inputModel.Name, inputModel.Cover);
            return this.Redirect("/Albums/All");

        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var albumWithTracks = this.albumsService.GetAlbumWithTracks(id);

            var albumToProject = new AlbumTracksDetailsViewModel
            {
                Id = albumWithTracks.Id,
                Cover = albumWithTracks.Cover,
                Name = albumWithTracks.Name,
                Price = albumWithTracks.Price,
                Tracks = albumWithTracks.Tracks
                .Select(t => new TrackDetailsViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToList()
            };

            return this.View(albumToProject);
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var allAlbums = new AlbumsAllViewModel
            {
                Albums = this.albumsService.GetAllAlbums()
                .Select(a => new AlbumsViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList()
            };

            if (!allAlbums.Albums.Any())
            {
                return this.Error("There are currently no albums.");
            }

            return this.View(allAlbums);
        }
    }
}
