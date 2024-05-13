using Company.Enum;

namespace Company.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsCompleted { get; set; }
    }
}
