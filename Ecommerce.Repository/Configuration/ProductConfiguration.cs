using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).HasMaxLength(100);
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)");        
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.ProductBrandId);
            builder.HasOne(p=>p.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}
