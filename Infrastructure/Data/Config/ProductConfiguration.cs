using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(prod => prod.Name).IsRequired();
            builder.Property(prod => prod.Description).IsRequired().HasMaxLength(250);
            builder.Property(prod => prod.Price).IsRequired().HasColumnType("decimal");
            builder.Property(prod => prod.PictureUrl).IsRequired();
            builder.HasOne(prod => prod.ProductBrand).WithMany().HasForeignKey(prod => prod.ProductBrandId);
            builder.HasOne(prod => prod.ProductType).WithMany().HasForeignKey(prod => prod.ProductTypeId);

        }
    }
}
