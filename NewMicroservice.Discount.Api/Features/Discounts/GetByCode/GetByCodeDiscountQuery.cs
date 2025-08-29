using NewMicroservice.Discount.Api.Features.Discounts.Dto;

namespace NewMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public record GetByCodeDiscountQuery(string Code) : IRequestByServiceResult<DiscountDto>;


}
