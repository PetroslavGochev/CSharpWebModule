using Microsoft.EntityFrameworkCore;
using SulsApp.Data.Model;

namespace SulsApp
{
    public class SulsAppDbContext : DbContext
    {
        public SulsAppDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SulsApp;Integrated Security=true");
        }
    }
}
