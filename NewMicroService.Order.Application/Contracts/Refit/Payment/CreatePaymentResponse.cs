namespace NewMicroservice.Order.Application.Contracts.Refit.Payment
{
    public record CreatePaymentResponse(Guid? PaymentId,bool Status, string? ErrorMessage);
}
