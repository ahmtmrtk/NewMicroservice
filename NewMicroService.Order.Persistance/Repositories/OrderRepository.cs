using Microsoft.EntityFrameworkCore;
using NewMicroService.Order.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Persistance.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public async Task<List<Domain.Entities.Order>> GetOrderByUserName(Guid buyerId)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return orders;
        }
    }
}
