using System.Net;
using MassTransit;
using MediatR;
using NewMicroservice.Bus.Events;
using NewMicroservice.Order.Application.Contracts.Refit.Payment;
using NewMicroservice.Shared;
using NewMicroservice.Shared.Services;
using NewMicroService.Order.Application.Contracts.Refit.Payment;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Application.Contracts.UnitOfWork;
using NewMicroService.Order.Domain.Entities;

namespace NewMicroService.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IIdentityService identityService, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint,
    IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
        if (!request.OrderItems.Any())
            return ServiceResult.Error("Order items not found", "Order must have at least one item",
                HttpStatusCode.BadRequest);


        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };


        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate,
            newAddress.Id);
        foreach (var orderItem in request.OrderItems)
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);


        order.Address = newAddress;


        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);


        CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.OrderCode, request.Payment.CardNumber,
            request.Payment.CardName, request.Payment.Expiration, request.Payment.CVV, order.TotalPrice);
        var paymentResponse = await paymentService.CreatePaymentAsync(paymentRequest);


        if (paymentResponse.Status == false)
            return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);


        order.MarkAsPaid(paymentResponse.PaymentId!.Value);

        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);


        await publishEndpoint.Publish(new OrderCreatedEvent(order.Id, identityService.GetUserId),
            cancellationToken);
        return ServiceResult.SuccessAsNoContent();
        }
    }
}