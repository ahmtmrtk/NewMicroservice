using NewMicroservice.Shared;

namespace NewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult;
}
