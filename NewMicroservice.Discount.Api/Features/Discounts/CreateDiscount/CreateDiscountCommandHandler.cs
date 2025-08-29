
using NewMicroservice.Discount.Api.Repositories;

namespace NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code);
            if (hasDiscount)
                return ServiceResult.Error("Discount already exists for this user and code", HttpStatusCode.Conflict);
            var discount = new Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Rate = request.Rate,
                UserId = request.UserId,
                ExpireDate = request.ExpireDate,
                CreatedDate = DateTime.UtcNow
            };
            await context.Discounts.AddAsync(discount);
            context.SaveChanges();
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
