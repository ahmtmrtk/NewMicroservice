using NewMicroservice.Shared;

namespace NewMicroservice.Order.Application.Contracts.Refit.Payment
{
    public record CreatePaymentRequest(string OrderCode,
        string CardNumber,
        string CardHolderName,
        string CardExpirationDate,
        string CardSecurityNumber,
        decimal Amount) : IRequestByServiceResult<CreatePaymentResponse>;
}
