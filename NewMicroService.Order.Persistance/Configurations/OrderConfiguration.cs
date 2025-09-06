using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Persistance.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {


        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.OrderCode).IsRequired().HasMaxLength(10);
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.BuyerId).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.AddressId).IsRequired();
            builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.DiscountRate).HasColumnType("float");

            builder.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId);
            builder.HasOne(o => o.Address).WithMany().HasForeignKey(o => o.AddressId);
        }
    }
}
