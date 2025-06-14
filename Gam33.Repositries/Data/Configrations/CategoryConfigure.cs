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
    public class CategoryConfigure : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired()
                                      .HasMaxLength(50);
            builder.Property(c => c.Description).IsRequired()
                                            .HasMaxLength(150);

            builder.HasOne(c => c.Admin)
                     .WithMany()
                     .HasForeignKey(c => c.AdminId)
                     .OnDelete(DeleteBehavior.SetNull);
               

        }
    }
}
