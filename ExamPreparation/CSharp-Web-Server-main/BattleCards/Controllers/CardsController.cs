namespace BattleCards.Controllers
{
    using System.Linq;

    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Controllers.Common.PathConstants;

    [Authorize]
    public class CardsController : Controller
    {
        private readonly ICardService cardService;
        private readonly IValidatorService validatorService;

        public CardsController(
            ICardService cardService,
            IValidatorService validatorService)
        {
            this.cardService = cardService;
            this.validatorService = validatorService;
        }

        public HttpResponse All()
        {
            var allCards = this.cardService.Cards();

            return this.View(allCards);
        }

        public HttpResponse Collection()
        {
            var allCards = this.cardService.UserCards(this.User.Id);

            return this.View(allCards);
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddViewModel model)
        {
            var errors = this.validatorService.CardValidation(model);

            if (errors.Any())
            {
                return this.Error(errors);
            }

            this.cardService.AddCard(model);

            return this.Redirect(AllCards);
        }

        public HttpResponse AddToCollection(string cardId)
        {
            this.cardService.AddToCollection(cardId, this.User.Id);

            return this.Redirect(AllCards);
        }

        public HttpResponse RemoveFromCollection(string cardId)
        {
            this.cardService.RemoveFromCollection(cardId, this.User.Id);

            return this.Redirect(CardsCollection);
        }
    }
}
