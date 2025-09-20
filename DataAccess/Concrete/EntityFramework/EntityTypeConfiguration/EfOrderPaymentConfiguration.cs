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
    public class EfOrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
    {
        public void Configure(EntityTypeBuilder<OrderPayment> builder)
        {
            builder.HasKey(op => op.Id);
            builder.Property(op => op.OrderId).IsRequired();
            builder.Property(op => op.Provider).IsRequired().HasMaxLength(100);
            builder.Property(op => op.TransactionId).IsRequired().HasMaxLength(200);
            builder.Property(op => op.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(op => op.Status).IsRequired();
            builder.Property(op => op.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.HasOne(op => op.Order).WithOne(o => o.OrderPayment).HasForeignKey<OrderPayment>(op => op.OrderId);
        }
    }
}
