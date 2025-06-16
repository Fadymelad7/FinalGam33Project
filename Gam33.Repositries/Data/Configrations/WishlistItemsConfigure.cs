using Gma33.Core.Entites.StoreEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data.Configrations
{
    public class WishlistItemsConfigure : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.HasOne(cp => cp.Product)
                     .WithMany(p => p.WishlistItems)
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Wishlist)
                 .WithMany(c => c.Items)
                 .HasForeignKey(c => c.WishlistId)
                 .OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(wi => new { wi.WishlistId, wi.ProductId });

        }
    }
}
