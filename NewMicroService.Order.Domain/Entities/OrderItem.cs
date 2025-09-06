using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Domain.Entities
{
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                throw new ArgumentNullException(nameof(productName), "ProductName cannot be empty");
            }
            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice cannot be less than zero");
            }
            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
        }
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException("UnitPrice cannot be less than zero");
            }
            this.UnitPrice = newPrice;
        }
        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("Discount percentage must be between 0 and 100");
            }
            var discountAmount = UnitPrice * (decimal)(discountPercentage / 100);
            UnitPrice -= discountAmount;
        }
        public bool IsSameProduct(OrderItem orderItem)
        {
            return this.ProductId == orderItem.ProductId;
        }
    }
}
