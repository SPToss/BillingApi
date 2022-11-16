using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.DiscountCalculators
{
    public class DefaultDiscountCalculator : IDiscountCalculator
    {
        public Task<decimal> CalculatePriceAfterDiscount(IOrder order)
        {
            return Task.FromResult(order.Amount);
        }
    }
}
