namespace IRunes.Services
{
    using IRunes.Data;
    using IRunes.Data.Models;
    using IRunes.ViewModel.Album;
    using IRunes.ViewModel.Track;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext db;

        public AlbumService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AllAlbumViewModel> All()
        {
            var albums = this.db.Albums.ToArray();

            var albumsCollecton = new List<AllAlbumViewModel>();

            foreach (var album in albums)
            {
                var albumViewModel = new AllAlbumViewModel()
                {
                    Id = album.Id,
                    Name = album.Name
                };

                albumsCollecton.Add(albumViewModel);
            }

            return albumsCollecton;
        }

        public void CreateAlbum(CreateAlbumViewModel model)
        {
            var album = new Album()
            {
                Name = model.Name,
                Cover = model.Cover,
            };

            this.db.Albums.Add(album);

            this.db.SaveChanges();
        }

        public DetailAlbumViewModel Detail(string id)
        {
            var album = this.db.Albums.Find(id);

            var tracks = this.db.Tracks
                .Where(t => t.Album.Id == id)
                .ToArray();

            var trackCollection = new List<TrackViewModel>();
            foreach (var track in tracks)
            {
                var trackViewModel = new TrackViewModel()
                {
                    Id = track.Id,
                    Name = track.Name,
                };

                trackCollection.Add(trackViewModel);
            }

            var albumViewModel = new DetailAlbumViewModel()
            {
                Name = album.Name,
                Price = album.Price,
                Tracks = trackCollection,
                Id = album.Id,
                Cover = album.Cover
            };

            return albumViewModel;
        }
    }
}
