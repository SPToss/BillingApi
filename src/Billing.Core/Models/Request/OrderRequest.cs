namespace XYZ.Billing.Core.Models.Request
{
    public class OrderRequest
    {
        public Guid Number { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentGateway { get; set; }
        public string? Description { get; set; }
    }
}
