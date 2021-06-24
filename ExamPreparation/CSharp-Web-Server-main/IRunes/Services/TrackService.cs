namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Data.Models;
    using IRunes.ViewModel.Track;

    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext db;

        public TrackService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateTrack(CreateTrackViewModel model)
        {
            var track = new Track()
            {
                Name = model.Name,
                Link = model.Link,
                Price = model.Price,
                AlbumId = model.AlbumId
            };

            this.db.Tracks.Add(track);

            this.db.SaveChanges();

            AlbumPrice(model.AlbumId, model.Price);
        }

        private void AlbumPrice(string albumId, decimal price)
        {
            var albums = this.db.Albums.Find(albumId);

            albums.Price += price;

            this.db.SaveChanges();
        }

        public DetailsTrackViewModel Details(string trackId)
        {
            var track = this.db.Tracks.Find(trackId);

            var trackViewModel = new DetailsTrackViewModel()
            {
                Id = track.Id,
                Name = track.Name,
                Price = track.Price,
                Link = track.Link
            };

            return trackViewModel;
        }
    }
}
