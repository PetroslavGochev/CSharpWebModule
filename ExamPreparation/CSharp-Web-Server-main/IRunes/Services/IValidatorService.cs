namespace IRunes.Services
{
    using IRunes.ViewModel.Album;
    using IRunes.ViewModel.Track;
    using IRunes.ViewModel.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidator(RegisterViewModel input);

        IEnumerable<string> AlbumValidator(CreateAlbumViewModel input);

        IEnumerable<string> TrackValidator(CreateTrackViewModel input);
    }
}
