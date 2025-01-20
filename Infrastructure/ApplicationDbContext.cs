using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>().ToTable("Jobs");
            modelBuilder.Entity<Job>().HasKey(j => j.Id);
            modelBuilder.Entity<Job>().Property(j => j.AnnualSalaryMin).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Job>().Property(j => j.AnnualSalaryMax).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Job>().Property(j => j.SalaryCurrency).IsRequired(false); // Allow null values
        }
    }
}
