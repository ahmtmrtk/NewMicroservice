using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NewMicroService.Order.Application.Contracts.Refit.Payment;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Application.Contracts.UnitOfWork;
using NewMicroService.Order.Domain.Entities;

namespace NewMicroservice.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var orders = orderRepository.Where(x => x.Status == OrderStatus.WaitingForPayment)
                    .ToList();

                foreach (var order in orders)
                {
                    var paymentStatusResponse = await paymentService.GetStatusAsync(order.OrderCode);

                    if (paymentStatusResponse.IsPaid!)
                    {
                        await orderRepository.SetStatus(order.OrderCode, paymentStatusResponse.PaymentId!.Value,
                            OrderStatus.Paid);
                        await unitOfWork.CommitAsync(stoppingToken);
                    }
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}