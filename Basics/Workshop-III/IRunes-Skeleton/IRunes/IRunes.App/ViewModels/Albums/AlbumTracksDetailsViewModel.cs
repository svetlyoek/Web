namespace IRunes.App.ViewModels.Albums
{
    using System.Collections.Generic;
    public class AlbumTracksDetailsViewModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public IEnumerable<TrackDetailsViewModel> Tracks { get; set; }
    }
}
