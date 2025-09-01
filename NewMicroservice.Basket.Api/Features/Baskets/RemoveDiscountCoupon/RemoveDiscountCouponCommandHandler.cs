
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public class RemoveDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {

            var basketAsString = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.ErrorAsNotFound();
            }
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            if (!basket!.Items.Any())
            {
                return ServiceResult.ErrorAsNotFound();
            }
            basket!.CancelDiscount();
            await basketService.CreateCacheAsync(basket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
