using Company.DTO.Product;

namespace Company.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public DTOProductGet Product { get; set; }
        public int Quantity { get; set; }
    }
}
