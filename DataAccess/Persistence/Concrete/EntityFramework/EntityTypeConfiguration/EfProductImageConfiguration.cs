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
    public class EfProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.ProductId).IsRequired();
            builder.Property(pi => pi.Url).IsRequired().HasMaxLength(500);
            builder.Property(pi => pi.IsPrimary).IsRequired().HasDefaultValue(false);
            builder.Property(pi => pi.SortOrder).IsRequired().HasDefaultValue(0);
            builder.Property(pi => pi.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(pi => pi.ProductId);
        }
    }
}
