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
    }
}
