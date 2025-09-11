using Asp.Versioning.Builder;
using NewMicroservice.Payment.Api.Feature.Payments.Create;
using NewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

namespace NewMicroservice.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint();
        }
    }
}
