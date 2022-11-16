using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<IPayment> ProcessOrder(IOrder order);
    }
}
