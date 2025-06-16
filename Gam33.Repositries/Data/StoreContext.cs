using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Entites.OrderModule;
using Gma33.Core.Entites.StoreEntites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<ApplicationUser>();
            modelBuilder.Ignore<Address>();
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProduct> cartProducts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        public DbSet<PaymentMethod> paymentMethods { get; set; }
        #region New Order Module Entites

        public DbSet<Order>Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        #endregion
    }
}
