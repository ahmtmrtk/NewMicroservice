using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Data;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache cache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var hasBasket = await cache.GetStringAsync(cacheKey, cancellationToken);
            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);
            if (string.IsNullOrEmpty(hasBasket))
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);
                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(hasBasket);
            var existBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);
            if (existBasketItem is not null)
            {
                currentBasket.Items.Remove(existBasketItem);

            }

            currentBasket.Items.Add(newBasketItem);


            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket), cancellationToken);
        }
    }
}

