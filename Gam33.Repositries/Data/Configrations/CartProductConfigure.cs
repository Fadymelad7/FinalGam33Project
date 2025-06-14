using Gma33.Core.Entites.StoreEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Repositries.Data.Configrations
{
    public class CartProductConfigure : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasOne(cp => cp.Product)
                     .WithMany(p => p.CartProducts)
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Cart)
                 .WithMany(c => c.CartProducts)
                 .HasForeignKey(c => c.CartId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(cp => new { cp.ProductId, cp.CartId });
        }
    }
}
