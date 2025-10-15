namespace NewMicroservice.Payment.Api.Feature.Payments.GetStatus;

public record GetPaymentStatusResponse(Guid? PaymentId,bool isPaid);