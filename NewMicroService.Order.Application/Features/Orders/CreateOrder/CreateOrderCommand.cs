using NewMicroservice.Shared;

namespace NewMicroService.Order.Application.Features.Orders.CreateOrder
{

    public record CreateOrderCommand(float? DiscountRate, AddressDto Address, PaymentDto Payment, List<OrderItemDto> OrderItems) : IRequestByServiceResult;



    public record AddressDto(string Province, string District, string Street, string ZipCode, string Line);
    public record PaymentDto(string CardName, string CardNumber, string Expiration, string CVV, decimal Amount);

    public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);
}