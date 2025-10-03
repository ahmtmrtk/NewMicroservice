using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        public string GetCacheKey()
        {
            return string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
        }
        public string GetCacheKey(Guid userId)
        {
            return string.Format(BasketConst.BasketCacheKey, userId);
        }
        public async Task<string?> GetBasketFromCacheAsync(CancellationToken cancellationToken)
        {

            var basketAsString = await distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
            return basketAsString;
        }
        public async Task CreateCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            await distributedCache.SetStringAsync(GetCacheKey(), JsonSerializer.Serialize(basket), cancellationToken);
        }
        public async Task DeleteBasketAsync(Guid userId)
        {
            await distributedCache.RemoveAsync(GetCacheKey(userId));
        }
    }
}
