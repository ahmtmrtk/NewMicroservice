using Asp.Versioning.Builder;
using NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;
using NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using NewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using NewMicroservice.Basket.Api.Features.Baskets.GetBasket;
using NewMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace NewMicroservice.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/basket")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponItemGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint()
                .RequireAuthorization("Password");
        }
    }
}
