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
    public class EfBasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.ExpiresAt);
            builder.HasOne(b => b.User).WithOne(u => u.Basket).HasForeignKey<Basket>(b => b.UserId);
        }
    }
}
