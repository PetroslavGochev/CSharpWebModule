using Panda.Data;
using Panda.Data.Models;
using Panda.ViewModels.Package;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class PackageService : IPackageService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public PackageService(
            ApplicationDbContext db,
            IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public void ChangeStatus(string id)
        {
            var package = this.db.Packages.Find(id);

            package.Status = "Delievered";

            this.db.SaveChanges();
        }

        public string Create(CreateViewModel model)
        {
            var package = new Package()
            {
                Description = model.Description,
                ShippingAddress = model.ShippingAddress,
                RecipientId = this.userService.UserId(model.RecipientName),
                Status = "Pending",
                Weight = model.Weight
            };

            this.db.Packages.Add(package);

            this.db.SaveChanges();

            return package.Id;
        }

        public IEnumerable<PackageViewModel> Package(string status)
        {
            var packages = this.db.Packages.Where(p => p.Status == status).ToArray();

            var collection = new List<PackageViewModel>();

            foreach (var package in packages)
            {
                var packageViewModel = new PackageViewModel()
                {
                    Id = package.Id,
                    Description = package.Description,
                    ShippingAddress = package.ShippingAddress,
                    Status = package.Status,
                    RecipientName = this.userService.CurrentUsername(package.RecipientId),
                    Weight = package.Weight,
                };

                collection.Add(packageViewModel);
            }

            return collection;
        }
    }
}
