using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IPaymentProcessorFactory 
    {
        IPaymentProcessor Create(IOrder order);
    }
}
