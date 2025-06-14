using Gma33.Core.Entites.IdentityEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data.Configrations
{
    public class AddressConfigure : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(A => A.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(A => A.LastName).IsRequired().HasMaxLength(100);
            builder.Property(A => A.Street).IsRequired().HasMaxLength(100);
            builder.Property(A => A.City).IsRequired().HasMaxLength(100);
            builder.Property(A => A.Country).IsRequired().HasMaxLength(100);
            builder.HasOne(A => A.AppUser)
                   .WithOne(u => u.Address)
                   .HasForeignKey<Address>(A => A.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
