using MassTransit;
using MediatR;
using NewMicroservice.Bus.Events;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Application.Contracts.UnitOfWork;
using NewMicroService.Order.Domain.Entities;

namespace NewMicroService.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderItems == null || !request.OrderItems.Any())
                return ServiceResult.Error("No order item found", System.Net.HttpStatusCode.BadRequest);

            var newAddress = new Domain.Entities.Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };


            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate);
            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
            }
            order.Address = newAddress;
            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            var paymentId = Guid.Empty;
            order.MarkAsPaid(paymentId);
            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.GetUserId), cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }
    }
}