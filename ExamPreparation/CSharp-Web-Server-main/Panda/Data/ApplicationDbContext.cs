namespace Panda.Data
{
    using Microsoft.EntityFrameworkCore;
    using Panda.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Panda;Integrated Security=True;");
            }
        }
    }
}
