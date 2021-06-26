namespace BattleCards.Data
{
    using BattleCards.Data.Models;
    using Microsoft.EntityFrameworkCore;

    using static ConfigurationConection;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Conection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>(entity =>
            {
                entity.HasKey(sc => new { sc.CardId, sc.UserId });
                entity
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserCards)
                .HasForeignKey(sc => sc.UserId);
                entity
                .HasOne(sc => sc.Card)
                .WithMany(c => c.UserCards)
                .HasForeignKey(sc => sc.CardId);
            });
        }
    }
}
