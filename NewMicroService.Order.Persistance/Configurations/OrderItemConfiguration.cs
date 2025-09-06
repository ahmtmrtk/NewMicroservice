using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewMicroService.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Persistance.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).UseIdentityColumn();
            builder.Property(oi => oi.ProductId).IsRequired();
            builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(oi => oi.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");

        }
    }

}
