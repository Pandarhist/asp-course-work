using Microsoft.EntityFrameworkCore;
using MusicShop.Models;

namespace MusicShop.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Staff { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasCheckConstraint("p_amount", "p_amount >= 0", c => c.HasName("CK_Products_Amount"));
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc => new { sc.OrderId, sc.ProductId });
            modelBuilder.Entity<ShoppingCart>()
                .HasCheckConstraint("sc_count", "sc_count >= 1", c => c.HasName("CK_Product_Count"));
        }
    }
}
