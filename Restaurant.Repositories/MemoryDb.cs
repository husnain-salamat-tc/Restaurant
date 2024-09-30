using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Restaurant.Repositories
{
    public class MemoryDb :DbContext
    {
        public MemoryDb(DbContextOptions<MemoryDb> options)
     : base(options) { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Reservation> reservations { get; set; }
    }

    public class MemoryDbContextFactory : IDesignTimeDbContextFactory<MemoryDb>
    {
        public MemoryDb CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MemoryDb>();
             optionsBuilder.UseSqlite("DataSource=mydatabase.db"); // Use SQLite for persistence

            return new MemoryDb(optionsBuilder.Options);
        }
    }

}



