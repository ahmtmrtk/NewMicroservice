using Asp.Versioning.Builder;
using NewMicroservice.Payment.Api.Feature.Payments.Create;
using NewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
using NewMicroservice.Payment.Api.Feature.Payments.GetStatus;

namespace NewMicroservice.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint().CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint()
                .GetPaymentStatusGroupItemEndpoint();
        }
    }
}
