using NewMicroservice.Shared;

namespace NewMicroservice.Payment.Api.Feature.Payments.GetStatus;

public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;