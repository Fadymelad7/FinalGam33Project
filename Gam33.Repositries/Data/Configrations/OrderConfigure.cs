using Gma33.Core.Entites.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data.Configrations
{
    public class OrderConfigure : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)
                .HasConversion(OS => OS.ToString(), OS => (OrderStatues)Enum.Parse(typeof(OrderStatues), OS));

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");

            builder.OwnsOne(o => o.ShippingAddress, A => A.WithOwner());

            builder.HasOne(O => O.DeliveryMethod)
                  .WithMany()
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
