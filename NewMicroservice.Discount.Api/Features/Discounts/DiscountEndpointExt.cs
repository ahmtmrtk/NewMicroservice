using Asp.Versioning.Builder;
using NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;
using NewMicroservice.Discount.Api.Features.Discounts.GetByCode;

namespace NewMicroservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/Discount").WithTags("Discount").WithApiVersionSet(apiVersionSet).CreateDiscountGroupItemEndpoint().GetByCodeDiscountGroupItemEndpoint();
        }
    }
}
