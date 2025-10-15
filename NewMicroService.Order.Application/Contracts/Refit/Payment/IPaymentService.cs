using NewMicroservice.Order.Application.Contracts.Refit.Payment;
using NewMicroservice.Order.Application.Contracts.Refit.PaymentService;
using Refit;

namespace NewMicroService.Order.Application.Contracts.Refit.Payment
{
    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);
        [Get("/api/v1/payments/status/{orderCode}")]
        Task<GetPaymentStatusResponse> GetStatusAsync(string orderCode);

    }
}