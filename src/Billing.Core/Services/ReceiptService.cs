using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services
{
    public class ReceiptService : IReceiptService
    {
        public async Task<IReceipt> GenerateReceipt(IPayment payment)
        {
            return new ReceiptModel
            {
                Amount = payment.FinalAmount,
                CreatedDate = DateTime.UtcNow,
                ReceiptId = Guid.NewGuid()
            };
        }
    }
}
