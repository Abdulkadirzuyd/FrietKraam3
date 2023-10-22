using FrietKraam3.Models;
using Microsoft.EntityFrameworkCore;


namespace FrietKraam3.Data
{
    public class FrietKraamContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; }

        public FrietKraamContext(DbContextOptions<FrietKraamContext> contextOptions) : base(contextOptions)
        {       
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            base.OnConfiguring(optionsBuilder);
        }


    }
}
