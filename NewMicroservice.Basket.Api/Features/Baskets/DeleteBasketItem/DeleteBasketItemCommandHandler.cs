using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {

            var hasBasket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (string.IsNullOrEmpty(hasBasket))
            {
                return ServiceResult.ErrorAsNotFound();
            }
            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(hasBasket);
            var existBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);
            if (existBasketItem is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }
            currentBasket.Items.Remove(existBasketItem);
            await basketService.CreateCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
