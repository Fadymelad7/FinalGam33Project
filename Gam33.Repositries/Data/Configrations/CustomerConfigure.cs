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
    public class CustomerConfigure : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName).IsRequired()
                                            .HasMaxLength(25);
            builder.Property(c => c.LastName).IsRequired()
                                   .HasMaxLength(30);

            builder.Property(c => c.Address).IsRequired()
                                           .HasMaxLength(100);

            builder.Property(c => c.Email).IsRequired()
                                    .HasMaxLength(225);

            builder.HasIndex(c => c.Email)
                      .IsUnique();

            builder.Property(c => c.Password).IsRequired()
                              .HasMaxLength(250);

            builder.Property(c => c.PhoneNumber).IsRequired()
                                               .HasMaxLength(14);

            builder.HasIndex(c => c.PhoneNumber)
                   .IsUnique();

            builder.HasOne(c => c.Admin)
                      .WithMany()
                      .HasForeignKey(c => c.AdminId)
                      .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
