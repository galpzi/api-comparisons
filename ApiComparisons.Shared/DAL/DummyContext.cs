using Microsoft.EntityFrameworkCore;

namespace ApiComparisons.Shared.DAL
{
    public class DummyContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DummyContext(DbContextOptions<DummyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(o => o.ID);
            modelBuilder.Entity<Person>()
                .HasMany(o => o.Transactions)
                .WithOne(o => o.Person)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Store>().HasKey(o => o.ID);
            modelBuilder.Entity<Store>()
                .HasMany(o => o.Products)
                .WithOne(o => o.Store)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasKey(o => new { o.ID });

            modelBuilder.Entity<Transaction>().HasKey(o => new { o.ID, o.PersonID });
            modelBuilder.Entity<Transaction>()
                .HasMany(o => o.Purchases)
                .WithOne(o => o.Transaction)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Purchase>().HasKey(o => new { o.ProductID, o.TransactionID });
        }
    }
}
