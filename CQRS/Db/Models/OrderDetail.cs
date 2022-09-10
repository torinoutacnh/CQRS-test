namespace CQRS.Db.Models
{
    public class OrderDetail : Entity
    {
        public Guid OrderId { get; init; }
        public virtual Order OrderItem { get; }

        public Guid ProductId { get; init; }
        public virtual Product ProductItem { get; }

        public decimal Amount { get; set; }
        public decimal Total { get; set; }

        public OrderDetail() { }

        public OrderDetail(Guid orderId, Guid productId, decimal amount, decimal total)
        {
            OrderId = orderId;
            ProductId = productId;
            Amount = amount;
            Total = total;
        }
    }
}
