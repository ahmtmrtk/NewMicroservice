using NewMicroservice.Order.Application.Contracts.Refit.Payment;
using Refit;

namespace NewMicroService.Order.Application.Contracts.Refit.Payment
{
    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);
    }
}