namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Data.Model;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }

        public DbSet<User> USers { get; set; }

        public DbSet<UserTrip> Usertrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SharedTrip;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(entity =>
            {
                entity.HasKey(sc => new { sc.TripId, sc.UserId });
                entity
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserTrip)
                .HasForeignKey(sc => sc.UserId);
                entity
                .HasOne(sc => sc.Trip)
                .WithMany(c => c.UserTrip)
                .HasForeignKey(sc => sc.TripId);
            });
        }
    }
}
