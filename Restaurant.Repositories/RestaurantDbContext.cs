using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Repositories;

namespace Restaurant.Repositories
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
        {
        }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-EQ55Q8H;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Customer and Reservation
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reservation>()
            .HasKey(r => r.Id);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
    {
        public RestaurantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-EQ55Q8H;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0");

            return new RestaurantDbContext(optionsBuilder.Options);
        }
    }

  
}


