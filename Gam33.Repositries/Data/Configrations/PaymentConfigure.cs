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
    public class PaymentConfigure : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(P => P.Amount).IsRequired()
                                             .HasColumnType("decimal(18,2)");
            builder.Property(P => P.PaymentStatus)
                   .IsRequired();





            builder.HasOne(p => p.PaymentMethod)
                   .WithMany()
                   .HasForeignKey(p => p.PaymentMethodId)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
