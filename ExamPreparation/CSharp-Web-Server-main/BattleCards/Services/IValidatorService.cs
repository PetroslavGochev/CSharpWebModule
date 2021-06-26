namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using BattleCards.ViewModels.Users;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidation(RegisterViewModel model);

        IEnumerable<string> CardValidation(AddViewModel model);
    }
}
