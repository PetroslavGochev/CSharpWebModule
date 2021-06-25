namespace Panda.Services
{
    using Panda.ViewModels.Package;
    using System.Collections.Generic;

    public interface IPackageService
    {
        string Create(CreateViewModel model);

        IEnumerable<PackageViewModel> Package(string status);

        void ChangeStatus(string id);
    }
}
