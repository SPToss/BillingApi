using AutoMapper;
using XYZ.Billing.Core.Models.Exceptions;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.PaymentProcessors
{
    public class DefaultPaymentProcessor : PaymentProcessorBase
    {
        public DefaultPaymentProcessor(IDiscountFactory discountFactory, IMapper mapper) : base(discountFactory, mapper)
        {
        }

        protected override Task<IPayment> ProcessPayment(IPayment payment)
        {
            throw new PaymentValidationException("Payment gateway not available");
        }
    }
}
