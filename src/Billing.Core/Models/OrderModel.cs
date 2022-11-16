using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Models
{
    public class OrderModel : IOrder
    {
        public Guid Number { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentGateway { get; set; }
        public string? Description { get; set; }
    }
}
