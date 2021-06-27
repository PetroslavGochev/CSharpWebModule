namespace Skeleton.Data
{
    using Microsoft.EntityFrameworkCore;
    using Skeleton.Data.Models;

    using static Common.ConfigurationConnection;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Conection);
            }
        }
    }
}
