using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace XYZ.Billing.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderValidator _orderValidator;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        private readonly IReceiptService _receiptService;

        public OrderService(
            IOrderValidator orderValidator, 
            IPaymentProcessorFactory paymentProcessorFactory, 
            IReceiptService receiptService)
        {
            _orderValidator = orderValidator;
            _paymentProcessorFactory = paymentProcessorFactory;
            _receiptService = receiptService;
        }

        public async Task<IReceipt> ProcessOrder(IOrder order)
        {
            await _orderValidator.Validate(order);

            var processor = _paymentProcessorFactory.Create(order);

            var payment = await processor.ProcessOrder(order);

            var receipt = await _receiptService.GenerateReceipt(payment);

            return receipt;
        }
    }
}
