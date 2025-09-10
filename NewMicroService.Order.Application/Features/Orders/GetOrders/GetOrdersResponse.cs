using NewMicroService.Order.Application.Features.Orders.CreateOrder;

namespace NewMicroService.Order.Application.Features.Orders.GetOrders
{
    public record GetOrdersResponse(DateTime OrderDate, decimal TotalPrice, List<OrderItemDto> Items);


}