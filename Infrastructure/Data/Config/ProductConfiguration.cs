using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p =>p.Id).IsRequired(); 
            builder.Property(p =>p.Name).IsRequired().HasMaxLength(100); 
            builder.Property(p =>p.Description).IsRequired().HasMaxLength(180); 
            builder.Property(p =>p.Price).HasColumnType("decimal(18,2)"); 
            builder.Property(p =>p.PictureUrl).IsRequired();
            builder.HasOne(b =>  b.ProductBrand).WithMany() ///one brand can associate with many product, Nike can have many product
            .HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(t => t.ProductType).WithMany()  //one product type can associate with many product.  Nike can create many shoes, jacket prodcut type
            .HasForeignKey(p => p.ProductTypeId);         
        }
    }
}