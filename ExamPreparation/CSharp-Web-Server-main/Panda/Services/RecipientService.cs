namespace Panda.Services
{
    using Panda.Data;
    using Panda.Data.Models;
    using Panda.ViewModels.Receipt;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class RecipientService : IRecipientService
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;

        public RecipientService(
            ApplicationDbContext db,
            IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public IEnumerable<RecipientViewModel> All(string userId)
        {
            var recipients = this.db.Receipts.Where(r => r.RecipientId == userId).ToList();

            var collection = new List<RecipientViewModel>();

            foreach (var rec in recipients)
            {
                var recipt = new RecipientViewModel()
                {
                    Id = rec.Id,
                    Fee = rec.Fee,
                    IssuedOn = rec.IssuedOn,
                    Recipient = this.userService.CurrentUsername(rec.RecipientId)
                };
                collection.Add(recipt);
            }

            return collection;
        }

        public void Create(string packageId)
        {
            var package = this.db.Packages.Find(packageId);

            var receptient = new Receipt()
            {
                Fee = (decimal)package.Weight * 2.67M,
                IssuedOn = DateTime.Now,
                PackageId = package.Id,
                RecipientId = package.RecipientId,
            };

            this.db.Receipts.Add(receptient);

            this.db.SaveChanges();
        }
    }
}
