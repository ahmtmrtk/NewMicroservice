using MediatR;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Application.Contracts.UnitOfWork;
using NewMicroService.Order.Application.Features.Orders.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IOrderRepository orderRepository, IIdentityService identityService) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByUserName(identityService.GetUserId);
            if (orders == null || !orders.Any())
                return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(new List<GetOrdersResponse>());

            var response = orders.Select(o => new GetOrdersResponse(
                o.CreatedDate,
                o.TotalPrice,
                o.OrderItems.Select(oi => new OrderItemDto(
                    oi.ProductId,
                    oi.ProductName,
                    oi.UnitPrice)).ToList())
            ).ToList();
            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}
