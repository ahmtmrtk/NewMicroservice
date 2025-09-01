using System.Text.Json.Serialization;

namespace NewMicroservice.Basket.Api.Dto
{
    public record BasketDto
    {
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }
        [JsonIgnore]
        public bool IsAppliedDiscount => DiscountRate > 0 && !string.IsNullOrEmpty(Coupon);

        public decimal TotalPrice => Items.Sum(item => item.Price);

        public decimal? TotalPriceByApplyDiscount
        {
            get
            {
                if (!IsAppliedDiscount) return null;
                return Items.Sum(item => item.PriceByApplyDiscountRate);
            }
        }

        public BasketDto(List<BasketItemDto> items)
        {

            Items = items;
        }
        public BasketDto()
        {

        }

    };


}
