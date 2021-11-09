using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_CCSB.Models.ViewModels;

namespace Project_CCSB.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<BlockedDates> BlockedDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(a => new { a.Date, a.LicensePlate });

            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Length)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Invoice>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Rate>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
