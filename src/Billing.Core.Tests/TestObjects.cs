using XYZ.Billing.Core.Models;

namespace Billing.Core.Tests
{
    public class TestObjects
    {
        public OrderModel GetOrderModel(decimal amount, int paymentGateWay)
        {
            return new OrderModel
            {
                Amount = amount,
                PaymentGateway = paymentGateWay,
                Description = "Some Description",
                Number = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
        }

        public PaymentModel GetPaymentModel(OrderModel order)
        {
            return new PaymentModel
            {
                Amount = order.Amount,
                PaymentGateway = order.PaymentGateway,
                Description = order.Description,
                Number = order.Number,
                UserId = order.UserId,
                FinalAmount = order.Amount
            };
        }

        public ReceiptModel GetReceiptModel(PaymentModel payment)
        {
            return new ReceiptModel
            {
                Amount = payment.FinalAmount,
                CreatedDate = DateTime.Now,
                ReceiptId = Guid.NewGuid()
            };
        }
    }
}
