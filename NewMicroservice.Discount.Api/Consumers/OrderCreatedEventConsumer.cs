using MassTransit;
using NewMicroservice.Discount.Api.Features.Discounts;
using NewMicroservice.Bus.Events;
using NewMicroservice.Discount.Api.Repositories;


namespace NewMicroservice.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var discount = new Features.Discounts.Discount
            {
                Id = Guid.CreateVersion7(),
                Code = DiscountCodeGenerator.Generate(10),
                Rate = 0.1f,
                UserId = context.Message.UserId,
                ExpireDate = DateTime.Now.AddMonths(1),
                CreatedDate = DateTime.UtcNow
            };
            await appDbContext.Discounts.AddAsync(discount);
            appDbContext.SaveChanges();
        }
    }
}
