using NewMicroservice.Shared;

namespace NewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
