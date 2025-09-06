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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(a => a.Province).IsRequired().HasMaxLength(50);
            builder.Property(a => a.District).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
            builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(100);

        }
    }
}
