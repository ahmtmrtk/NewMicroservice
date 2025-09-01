
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.ErrorAsNotFound();
            }
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            if (!basket!.Items.Any())
            {
                return ServiceResult.ErrorAsNotFound();
            }
            basket!.ApplyDiscount(request.Rate, request.Coupon);
            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket), cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
