using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Request;
using XYZ.Billing.Core.Models.Response;

namespace Billing.Api.Tests
{
    public class TestObjects
    {
        public OrderRequest GetOrderRequest()
        {
            return new OrderRequest
            {
                Amount = 1,
                PaymentGateway = 1,
                Description = "Test",
                Number = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
        }

        public OrderModel GetOrderModel(OrderRequest request)
        {
            return new OrderModel
            {
                Amount = request.Amount,
                PaymentGateway = request.PaymentGateway,
                Description = request.Description,
                UserId = request.UserId,
                Number = request.Number,
            };
        }

        public ReceiptModel GetReceiptModel()
        {
            return new ReceiptModel
            {
                CreatedDate = DateTime.Now,
                Amount = 1,
                ReceiptId = Guid.NewGuid()
            };
        }

        public ReceiptResponse GetReceiptResponse()
        {
            return new ReceiptResponse
            {
                CreatedDate = DateTime.Now,
                Amount = 1,
                ReceiptId = Guid.NewGuid()
            };
        }
    }
}
