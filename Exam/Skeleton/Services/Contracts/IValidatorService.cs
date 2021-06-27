namespace Skeleton.Services.Contracts
{
    using Skeleton.ViewModels.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidation(RegisterViewModel model);
    }
}
