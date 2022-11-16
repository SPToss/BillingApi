namespace XYZ.Billing.Core.Models.Interfaces
{
    public interface IReceipt
    {
        Guid ReceiptId { get; set; }

        decimal Amount { get; set; }

        DateTime CreatedDate { get; set; }
    }
}
