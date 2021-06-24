namespace IRunes.Services
{
    using IRunes.ViewModel.Album;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        IEnumerable<AllAlbumViewModel> All();

        void CreateAlbum(CreateAlbumViewModel model);

        DetailAlbumViewModel Detail(string id);
    }
}
