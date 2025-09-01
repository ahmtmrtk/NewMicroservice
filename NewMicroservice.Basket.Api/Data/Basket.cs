namespace NewMicroservice.Basket.Api.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public Basket(Guid userId, List<BasketItem> items)
        {
            UserId = userId;
            Items = items;

        }
        public Basket()
        {
        }
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

        public void ApplyDiscount(float discountRate, string coupon)
        {
            DiscountRate = discountRate;
            Coupon = coupon;
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
            }
        }
        public void ApplyAvaiableDiscount()
        {
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate);
            }
        }
        public void CancelDiscount()
        {
            DiscountRate = null;
            Coupon = null;
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = null;
            }
        }




    }
}
