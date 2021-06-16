namespace MyWebServer.Controllers
{
    using MyWebServer.Model.Animals;
    using MyWebServer.Service.Controllers;
    using MyWebServer.Service.Http;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey)
                ? query[nameKey]
                : "the cats";

            var catAge = query.ContainsKey(ageKey)
                ? int.Parse(query[ageKey])
                : 0;

            var viewModel = new CatViewModel()
            {
                Name = catName,
                Age = catAge,
            };

            return this.View(viewModel);
        }

        public HttpResponse Create()
            => this.View();

        public HttpResponse Dogs()
            => this.View(new DogViewModel()
            {
                Name = "Sharo",
                Age = 4,
                Breed = "Street Perfect",
            });

        public HttpResponse Bunnies()
            => this.View("Rabbits");

        public HttpResponse Turtles()
            => this.View("Animals/Wild/Turtles");
    }
}
