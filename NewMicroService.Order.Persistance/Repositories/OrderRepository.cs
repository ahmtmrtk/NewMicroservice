using Microsoft.EntityFrameworkCore;
using NewMicroService.Order.Application.Contracts.Repositories;
using NewMicroService.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Persistance.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
    {
        return context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId)
            .OrderByDescending(x => x.CreatedDate).ToListAsync();
    }

    public async Task SetStatus(string orderCode, Guid paymentId, OrderStatus status)
    {
        var order = await context.Orders.FirstAsync(x => x.OrderCode == orderCode);

        order.Status = status;
        order.PaymentId = paymentId;
        context.Update(order);
    }
    }
}
