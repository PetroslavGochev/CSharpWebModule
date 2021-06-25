namespace Panda.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using Panda.Services;
    using Panda.ViewModels.Package;
    using System.Linq;

    [Authorize]
    public class PackagesController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidatorService validatorService;
        private readonly IPackageService packageService;
        private readonly IRecipientService recipientService;

        public PackagesController(
            IUserService userService,
            IValidatorService validatorService,
            IPackageService packageService,
            IRecipientService recipientService)
        {
            this.userService = userService;
            this.validatorService = validatorService;
            this.packageService = packageService;
            this.recipientService = recipientService;
        }
        public HttpResponse Create()
        {
            var model = this.userService.AllUsers();
            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(CreateViewModel model)
        {
            var errors = this.validatorService.PackageValidator(model);

            if (errors.Any())
            {
                return this.Redirect("/Packages/Create");
            }

            var packageId = this.packageService.Create(model);
            this.recipientService.Create(packageId);

            return this.Redirect("/Packages/Pending");
        }

        public HttpResponse Pending()
        {
            var model = this.packageService.Package("Pending");
            return this.View(model);
        }

        public HttpResponse Delivered()
        {
            var model = this.packageService.Package("Delievered");
            return this.View(model);
        }

        public HttpResponse Deliver(string id)
        {
            this.packageService.ChangeStatus(id);
            return this.Redirect("/Packages/Delivered");
        }
    }
}
