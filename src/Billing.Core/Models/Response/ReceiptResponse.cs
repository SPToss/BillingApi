namespace XYZ.Billing.Core.Models.Response
{
    public class ReceiptResponse
    { 
        public Guid ReceiptId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
