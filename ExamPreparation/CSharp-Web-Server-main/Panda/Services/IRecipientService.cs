namespace Panda.Services
{
    using Panda.ViewModels.Receipt;
    using System.Collections.Generic;

    public interface IRecipientService
    {
        IEnumerable<RecipientViewModel> All(string userId);

        void Create(string packageId);
    }
}
