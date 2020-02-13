namespace IRunes.App.Controllers.Tracks
{
    using IRunes.App.ViewModels.Tracks;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;
        private readonly IAlbumsService albumService;

        public TracksController(ITracksService tracksService, IAlbumsService albumService)
        {
            this.tracksService = tracksService;
            this.albumService = albumService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var album = this.albumService.GetAlbumById(albumId);

            var albumToProject = new TrackCreateViewModel
            {
                AlbumId = album.Id,

            };

            return this.View(albumToProject);
        }

        [HttpPost]
        public HttpResponse Create(TrackCreateInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return this.Error("name must be between 4 and 20 characters!");
            }
            if (string.IsNullOrWhiteSpace(inputModel.Link))
            {
                return this.Error("Link is required!");
            }
            if (inputModel.Price < 0)
            {
                return this.Error("Price must be a positive number!");
            }

            this.tracksService.Create(inputModel.Name, inputModel.Price, inputModel.Link, inputModel.AlbumId);

            return this.Redirect("/Albums/Details?id=" + inputModel.AlbumId);

        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var track = this.tracksService.GetById(id);

            var trackToProject = new TrackDetailsViewModel
            {
                AlbumId = track.AlbumId,
                Link = track.Link,
                Name = track.Name,
                Price = track.Price
            };

            if (track == null)
            {
                return this.Error("Track not found!");
            }

            return this.View(trackToProject);
        }
    }
}
