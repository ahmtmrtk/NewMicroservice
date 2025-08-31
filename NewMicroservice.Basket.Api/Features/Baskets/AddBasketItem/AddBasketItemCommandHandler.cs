using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache cache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.NewGuid();
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var hasBasket = await cache.GetStringAsync(cacheKey, cancellationToken);
            BasketDto? currentBasket;
            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);
            if (string.IsNullOrEmpty(hasBasket))
            {
                currentBasket = new BasketDto(userId, [newBasketItem]);
                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<BasketDto>(hasBasket);
            var existBasketItem = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);
            if (existBasketItem is not null)
            {
                currentBasket.BasketItems.Remove(existBasketItem);

            }

            currentBasket.BasketItems.Add(newBasketItem);


            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
        private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
        {
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket), cancellationToken);
        }
    }
}

