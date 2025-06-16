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
    public class WishlistConfigure : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.Property(c => c.BuyerEmail)
                       .IsRequired();
        }
    }
}
