using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var hasBasket = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(hasBasket))
            {
                return ServiceResult.ErrorAsNotFound();
            }
            var currentBasket = JsonSerializer.Deserialize<BasketDto>(hasBasket);
            var existBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);
            if (existBasketItem is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }
            currentBasket.Items.Remove(existBasketItem);
            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(currentBasket), cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
