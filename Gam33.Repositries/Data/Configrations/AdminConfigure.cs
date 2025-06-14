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
    public class AdminConfigure : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(A => A.FirstName).IsRequired()
                                   .HasMaxLength(50);

            builder.Property(A=>A.LastName).IsRequired()
                                 .HasMaxLength(70);

            builder.Property(A => A.Address).IsRequired()
                                        .HasMaxLength(100);

            builder.Property(A => A.Email).IsRequired()
                                    .HasMaxLength(225);

            builder.HasIndex(A => A.Email)
                      .IsUnique();

            builder.Property(A => A.Password).IsRequired()
                              .HasMaxLength(250);

            builder.Property(A => A.PhoneNumber).IsRequired()
                                               .HasMaxLength(14);

            builder.HasIndex(A => A.PhoneNumber)
                   .IsUnique();

        }
    }
}
