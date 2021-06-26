namespace BattleCards.Services
{
    using System.Linq;

    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public class CardService : ICardService
    {
        private readonly ApplicationDbContext db;

        public CardService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCard(AddViewModel model)
        {
            var card = new Card()
            {
                Name = model.Name,
                Keyword = model.Keyword,
                Attack = model.Attack,
                Health = model.Health,
                Descritpion = model.Description,
                ImageUrl = model.Image,
            };

            this.db.Cards.Add(card);

            this.db.SaveChanges();
        }

        public void AddToCollection(string cardId, string userId)
        {
            if (!this.IsUserHaveCard(userId, cardId))
            {
                var userCards = CreateUserCards(cardId, userId);

                this.db.UserCards.Add(userCards);

                this.db.SaveChanges();
            }
        }

        public IEnumerable<CardViewModel> Cards()
        {
            var cards = this.db.Cards
                .ToArray();

            var cardCollection = new List<CardViewModel>();

            foreach (var card in cards)
            {
                var cardViewModel = new CardViewModel()
                {
                    Keyword = card.Keyword,
                    Health = card.Health,
                    Attack = card.Attack,
                    Description = card.Descritpion,
                    Id = card.Id,
                    ImageUrl = card.ImageUrl,
                };

                cardCollection.Add(cardViewModel);
            }

            return cardCollection;
        }

        public IEnumerable<CardViewModel> UserCards(string id)
        {
            var userCards = this.db.UserCards
                .Where(uc => uc.UserId == id)
                .Select(c => c.CardId)
                .ToArray();

            var cardViewModelCollection = new List<CardViewModel>();

            foreach (var cardId in userCards)
            {
                var card = this.db.Cards.Find(cardId);

                var cardVideModel = new CardViewModel()
                {
                    Id = card.Id,
                    ImageUrl = card.ImageUrl,
                    Attack = card.Attack,
                    Health = card.Health,
                    Description = card.Descritpion,
                    Keyword = card.Keyword,
                };

                cardViewModelCollection.Add(cardVideModel);
            }

            return cardViewModelCollection;
        }

        public void RemoveFromCollection(string cardId, string userId)
        {
            if (this.IsUserHaveCard(userId, cardId))
            {
                UserCard userCards = CreateUserCards(cardId, userId);

                this.db.UserCards.Remove(userCards);

                this.db.SaveChanges();
            }
        }

        private static UserCard CreateUserCards(string cardId, string userId)
        {
            return new UserCard()
            {
                CardId = cardId,
                UserId = userId,
            };
        }

        private bool IsUserHaveCard(string userId, string cardId)
        {
            return this.db.UserCards.Any(uc => uc.UserId == userId && uc.CardId == cardId);
        }
    }
}
