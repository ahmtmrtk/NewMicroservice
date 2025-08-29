using NewMicroservice.Discount.Api.Features.Discounts.Dto;
using NewMicroservice.Discount.Api.Repositories;

namespace NewMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public class GetByCodeDiscountHandler(AppDbContext context) : IRequestHandler<GetByCodeDiscountQuery, ServiceResult<DiscountDto>>
    {
        public async Task<ServiceResult<DiscountDto>> Handle(GetByCodeDiscountQuery request, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.FirstOrDefaultAsync(x => x.Code == request.Code);
            if (discount == null)
                return ServiceResult<DiscountDto>.Error("Discount not found", HttpStatusCode.NotFound);
            var discountDto = new DiscountDto
            {
                UserId = discount.UserId,
                Rate = discount.Rate,
                Code = discount.Code,
                CreatedDate = discount.CreatedDate,
                UpdatedDate = discount.UpdatedDate,
                ExpireDate = discount.ExpireDate
            };
            return ServiceResult<DiscountDto>.SuccessAsOk(discountDto);
        }
    }
}
