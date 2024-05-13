using Company.Enum;

namespace Company.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
    }
}
