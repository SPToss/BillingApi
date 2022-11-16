using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IReceiptService
    {
        Task<IReceipt> GenerateReceipt(IPayment payment);
    }
}
