using NewMicroservice.Shared;

namespace NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl) : IRequestByServiceResult;


}
