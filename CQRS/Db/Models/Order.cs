namespace CQRS.Db.Models
{
    public class Order : Entity
    {
        public Guid CustomerId { get; init; }
        public Customer CustomerRef { get; }

        public decimal Total { get; set; }

        public virtual ICollection<OrderDetail> Details { get; }

        public Order() { } 

        public Order(Guid customerId, decimal total)
        {
            CustomerId = customerId;
            Total = total;
        }
    }
}
