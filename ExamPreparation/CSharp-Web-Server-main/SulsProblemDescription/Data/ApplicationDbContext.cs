namespace SulsApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using SulsProblemDescription.Data.Model;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SulsApp;Integrated Security=True;");
            }
        }
    }
}