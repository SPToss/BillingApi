using AutoMapper;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.PaymentProcessors
{
    public class WePayPaymentProcessor : PaymentProcessorBase
    {
        public WePayPaymentProcessor(IDiscountFactory discountFactory, IMapper mapper) : base(discountFactory, mapper)
        {
        }

        protected override async Task<IPayment> ProcessPayment(IPayment payment)
        {
            // TODO Actual logic here
            return payment;
        }
    }
}
