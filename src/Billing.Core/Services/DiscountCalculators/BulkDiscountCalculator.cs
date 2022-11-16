using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.DiscountCalculators
{
    public class BulkDiscountCalculator : IDiscountCalculator
    {
        public Task<decimal> CalculatePriceAfterDiscount(IOrder order)
        {
            return Task.FromResult(order.Amount * 0.95m);
        }
    }
}
