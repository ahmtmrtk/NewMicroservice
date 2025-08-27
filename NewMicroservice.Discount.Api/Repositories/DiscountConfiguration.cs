using MongoDB.EntityFrameworkCore.Extensions;
using NewMicroservice.Discount.Api.Features.Discounts;

namespace NewMicroservice.Discount.Api.Repositories
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Features.Discounts.Discount>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Features.Discounts.Discount> builder)
        {
            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
            builder.Property(x => x.Rate).HasElementName("rate");
            builder.Property(x => x.CreatedDate).HasElementName("createdDate");
            builder.Property(x => x.UserId).HasElementName("user_id");
            builder.Property(x => x.UpdatedDate).HasElementName("updatedDate");
            builder.Property(x => x.ExpireDate).HasElementName("expireDate");
            
        }
    }
}
