using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroService.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string OrderCode { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public float? DiscountRate { get; set; }
        public Guid? PaymentId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; } = null!;


        public static string GenerateOrderCode()
        {
            var random = new Random();
            var ordercode = new StringBuilder(10);
            for (int i = 0; i < 10; i++)
            {
                ordercode.Append(random.Next(0, 10));
            }
            return ordercode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
        {
            return new Order()
            {
                Id = Guid.CreateVersion7(),
                OrderCode = GenerateOrderCode(),
                CreatedDate = DateTime.UtcNow,
                BuyerId = buyerId,
                AddressId = addressId,
                TotalPrice = 0,
                Status = OrderStatus.WaitingForPayment,
                DiscountRate = discountRate,

            };
        }
        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate)
        {
            return new Order()
            {
                Id = Guid.CreateVersion7(),
                OrderCode = GenerateOrderCode(),
                CreatedDate = DateTime.UtcNow,
                BuyerId = buyerId,
                TotalPrice = 0,
                Status = OrderStatus.WaitingForPayment,
                DiscountRate = discountRate,

            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);

            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(oi => oi.UnitPrice);
            if (DiscountRate.HasValue && DiscountRate > 0)
            {
                TotalPrice -= TotalPrice * (decimal)(DiscountRate.Value / 100);

            }
        }


        public void MarkAsPaid(Guid paymentId)
        {
            if (Status != OrderStatus.WaitingForPayment)
            {
                throw new InvalidOperationException("Only orders waiting for payment can be marked as paid.");
            }
            Status = OrderStatus.Paid;
            this.PaymentId = paymentId;
        }




    }
}
