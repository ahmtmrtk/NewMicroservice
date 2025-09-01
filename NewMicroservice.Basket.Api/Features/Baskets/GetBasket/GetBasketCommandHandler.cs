using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketData = await distributedCache.GetStringAsync(cacheKey);
            var basketDto = new BasketDto(userId, new List<BasketItemDto>());
            if (basketData == null)
            {
                return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
            }
            basketDto = JsonSerializer.Deserialize<BasketDto>(basketData)!;
            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);

        }
    }
}
