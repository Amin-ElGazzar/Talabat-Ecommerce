using Ecommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Configuration
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryOrderMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryOrderMethod> builder)
        {
           builder.Property(m=>m.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
