using NewMicroservice.Basket.Api.Dto;

namespace NewMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery() : IRequestByServiceResult<BasketDto>;


}
