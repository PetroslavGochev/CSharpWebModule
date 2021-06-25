namespace Panda.Services
{
    using Panda.ViewModels.Package;
    using Panda.ViewModels.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidator(RegisterViewModel model);

        IEnumerable<string> PackageValidator(CreateViewModel input);

        //        IEnumerable<string> TrackValidator(CreateTrackViewModel input);
    }
}
