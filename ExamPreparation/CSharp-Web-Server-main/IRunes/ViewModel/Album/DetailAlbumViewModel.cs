using IRunes.ViewModel.Track;
using System.Collections.Generic;

namespace IRunes.ViewModel.Album
{
    public class DetailAlbumViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<TrackViewModel> Tracks { get; set; }
    }
}
