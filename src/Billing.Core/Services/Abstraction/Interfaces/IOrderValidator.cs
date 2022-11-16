using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IOrderValidator
    {
        Task Validate(IOrder order);
    }
}
