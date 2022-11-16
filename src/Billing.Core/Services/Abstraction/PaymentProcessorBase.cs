using AutoMapper;
using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction
{
    public abstract class PaymentProcessorBase : IPaymentProcessor
    {
        private readonly IDiscountFactory _discountFactory;
        private readonly IMapper _mapper;

        protected PaymentProcessorBase(IDiscountFactory discountFactory, IMapper mapper)
        {
            _discountFactory = discountFactory;
            _mapper = mapper;
        }

        protected abstract Task<IPayment> ProcessPayment(IPayment payment);

        public async Task<IPayment> ProcessOrder(IOrder order)
        {
            var discountCalculator = _discountFactory.Create(order);

            var finalAmount = await discountCalculator.CalculatePriceAfterDiscount(order);

            var payment = _mapper.Map<PaymentModel>(order);
            payment.FinalAmount = finalAmount;

            await ProcessPayment(payment);

            return payment;
        }
    }
}
