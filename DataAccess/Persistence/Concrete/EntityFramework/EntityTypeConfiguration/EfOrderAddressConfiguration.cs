using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Concrete.EntityFramework.EntityTypeConfiguration
{
    public class EfOrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.HasKey(oa => oa.Id);
            builder.Property(oa => oa.OrderId).IsRequired();
            builder.Property(oa => oa.AddressType).IsRequired().HasMaxLength(50);
            builder.Property(oa => oa.Street).IsRequired().HasMaxLength(500);
            builder.Property(oa => oa.City).IsRequired().HasMaxLength(200);
            builder.Property(oa => oa.Country).IsRequired().HasMaxLength(100);
            builder.Property(oa => oa.PostalCode).IsRequired().HasMaxLength(50);
            builder.HasOne(oa => oa.Order)
                .WithMany(o => o.OrderAddresses)
                .HasForeignKey(oa => oa.OrderId);

        }
    }
}
