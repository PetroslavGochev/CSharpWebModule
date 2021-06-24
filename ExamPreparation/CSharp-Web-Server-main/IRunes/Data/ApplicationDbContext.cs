namespace IRunes.Data
{
    using IRunes.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public class ApplicationDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=IRunes;Integrated Security=True;");
            }
        }
    }
}
