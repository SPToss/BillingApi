using XYZ.Billing.Core.Models.Exceptions;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.Validation
{
    public class OrderValidator : IOrderValidator
    {
        public async Task Validate(IOrder order)
        {
            if (order.Number == Guid.Empty)
            {
                throw new OrderValidationException("Invalid order number");
            }

            if (order.UserId == Guid.Empty)
            {
                throw new OrderValidationException("Invalid user");
            }

            if (order.Amount <= 0)
            {
                throw new OrderValidationException("Order amount must be positive");
            }

            // Any other validations

            return;
        }
    }
}
