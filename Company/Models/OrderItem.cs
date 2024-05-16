using Company.DTO.Product;

namespace Company.Models
{
    public class OrderItem
    {
        public required int Id { get; set; }
        public required DTOProductGet Product { get; set; }
        public required int Quantity { get; set; }
    }
}
