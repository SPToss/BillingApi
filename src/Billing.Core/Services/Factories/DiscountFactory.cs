using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.DiscountCalculators;

namespace XYZ.Billing.Core.Services.Factories
{
    public class DiscountFactory : IDiscountFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public DiscountFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDiscountCalculator Create(IOrder order)
        {
            // Check here if order qualify for any discount e.g. for users that make more than 1000 orders per month ( await _userService.GetAverageUserOrdersAmount(order.UserId)

            if (order.Amount > 100_000_000m)
            {
                return (IDiscountCalculator)_serviceProvider.GetService(typeof(BulkDiscountCalculator))!;
            }

            return (IDiscountCalculator)_serviceProvider.GetService(typeof(DefaultDiscountCalculator))!;
        }
    }
}
