namespace IRunes.Services
{
    using IRunes.ViewModel.Track;

    public interface ITrackService
    {
        void CreateTrack(CreateTrackViewModel model);

        DetailsTrackViewModel Details(string trackId);
    }
}
