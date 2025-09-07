using MediatR;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Domain.Entities;

namespace NewMicroService.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IGenericRepository<Guid,Domain.Entities.Order> orderRepository,IGenericRepository<int,Address>addressRepository,IIdentityService identityService)  : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.orderItems == null || !request.orderItems.Any())
                return Task.FromResult(ServiceResult.Error("No order item found", System.Net.HttpStatusCode.BadRequest));
            //TODO: transaction
            var newAddress = new Domain.Entities.Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };
            addressRepository.Add(newAddress);
            //unitOfWork.Commit();

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);
            foreach (var item in request.orderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
            }
            orderRepository.Add(order);
            //unitOfWork.Commit();
            var paymentId = Guid.Empty;
            // call payment service

            order.MarkAsPaid(paymentId);
            orderRepository.Update(order);
            return Task.FromResult(ServiceResult.SuccessAsNoContent());




        }
    }
}