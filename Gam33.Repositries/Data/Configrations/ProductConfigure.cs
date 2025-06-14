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
    public class ProductConfigure : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.ProductName).IsRequired()
                                              .HasMaxLength(400);

            builder.Property(p => p.Details).IsRequired()
                                          .HasMaxLength(500);

            builder.Property(p => p.Price)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
                    .HasColumnType("nvarchar(max)");

            builder.HasOne(p => p.Category)
                         .WithMany(p => p.Products)
                         .HasForeignKey(p => p.CategoryId)
                         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
