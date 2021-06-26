namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardService
    {
        IEnumerable<CardViewModel> Cards();

        IEnumerable<CardViewModel> UserCards(string id);

        void AddCard(AddViewModel model);

        void AddToCollection(string cardId, string userId);

        void RemoveFromCollection(string cardId, string userId);
    }
}
