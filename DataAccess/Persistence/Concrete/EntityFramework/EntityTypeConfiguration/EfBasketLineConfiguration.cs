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
    public class EfBasketLineConfiguration : IEntityTypeConfiguration<BasketLine>
    {
        public void Configure(EntityTypeBuilder<BasketLine> builder)
        {
            builder.HasKey(bl => bl.Id);
            builder.Property(bl => bl.BasketId).IsRequired();
            builder.Property(bl => bl.ProductId).IsRequired();
            builder.Property(bl => bl.Quantity).IsRequired().HasDefaultValue(1);
            builder.HasOne(bl => bl.Basket).WithMany(b => b.BasketLines).HasForeignKey(bl => bl.BasketId);
            builder.HasOne(bl => bl.Product).WithMany(b=> b.BasketLines).HasForeignKey(bl => bl.ProductId);
        }
    }
}
