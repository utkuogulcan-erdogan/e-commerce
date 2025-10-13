using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.EntityTypeConfiguration
{
    public class EfOrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(ol => ol.Id);
            builder.Property(ol => ol.OrderId).IsRequired();
            builder.Property(ol => ol.ProductId).IsRequired();
            builder.Property(ol => ol.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(ol => ol.Quantity).IsRequired();
            builder.Property(ol => ol.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(ol => ol.LineTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasComputedColumnSql("[Quantity] * [UnitPrice]"); 
            builder.HasOne(ol => ol.Order).WithMany(o => o.OrderLines).HasForeignKey(ol => ol.OrderId);
            builder.HasOne(ol => ol.Product).WithMany(p => p.OrderLines).HasForeignKey(ol => ol.ProductId);

        }
    }
}
