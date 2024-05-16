using Company.Enum;

namespace Company.Models
{
    public class Product
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public required bool Available { get; set; }
        public required Category Category { get; set; } 
        public required decimal Quantity { get; set; } 
        public required Unit Unit { get; set; }
    }
}
