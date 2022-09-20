using MauiApi.Domain.Base;

namespace MauiApi.Domain.Entities.Orders
{
    public class OrderInfo : EntityBase
    {
        public string StoreId { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string OrderNo { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public string AccountId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public DateTime? PayTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string Address { get; set; } = null!;
        public int OrderStatus { get; set; }
    }
    public class OrderItem : EntityBase
    {
        public string OrderId { get; set; } = null!;
        public string OrderNo { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
