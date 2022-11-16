using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Models
{
    
    public class ReceiptModel : IReceipt
    {
        public Guid ReceiptId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
