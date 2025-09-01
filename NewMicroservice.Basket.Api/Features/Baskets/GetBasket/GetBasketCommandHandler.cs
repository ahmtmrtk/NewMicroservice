using Microsoft.Extensions.Caching.Distributed;
using NewMicroservice.Basket.Api.Const;
using NewMicroservice.Basket.Api.Dto;
using NewMicroservice.Shared.Services;
using System.Text.Json;

namespace NewMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketCommandHandler(IMapper mapper, BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {

            var basketData = await basketService.GetBasketFromCacheAsync(cancellationToken);
            var basketDto = new BasketDto(new List<BasketItemDto>());
            if (basketData == null)
            {
                return ServiceResult<BasketDto>.Error("Basket not found.", HttpStatusCode.NotFound);
            }
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketData)!;
            basketDto = mapper.Map<BasketDto>(basket);
            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);

        }
    }
}
