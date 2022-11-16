using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using XYZ.Billing.Api.Controllers;
using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Models.Request;
using XYZ.Billing.Core.Models.Response;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace Billing.Api.Tests
{
    public class OrderControllerTests : IDisposable
    {
        private readonly TestObjects _testObjects;
        private readonly OrderController _sut;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IOrderService> _orderService;

        public OrderControllerTests()
        {
            _testObjects = new TestObjects();
            _mapper = new Mock<IMapper>();
            _orderService = new Mock<IOrderService>();

            _sut = new OrderController(_orderService.Object, _mapper.Object);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task ProcessOrder_WhenCorrectRequest_ShouldComplete()
        {
            //Arrange
            var request = _testObjects.GetOrderRequest();
            var order = _testObjects.GetOrderModel(request);
            var receipt = _testObjects.GetReceiptResponse();
            var receiptModel = _testObjects.GetReceiptModel();

            _mapper.Setup(x => x.Map<OrderModel>(It.IsAny<OrderRequest>())).Returns(order);
            _mapper.Setup(x => x.Map<ReceiptResponse>(It.IsAny<ReceiptModel>())).Returns(receipt);
            _orderService.Setup(x => x.ProcessOrder(It.IsAny<IOrder>())).ReturnsAsync(receiptModel);

            //Act
            var results = await _sut.ProcessOrder(request);

            //Arrange
            results.Should().NotBeNull();
            results.Should().BeOfType<OkObjectResult>().Which.Should().BeEquivalentTo(new OkObjectResult(receipt));

            _mapper.Verify(x => x.Map<OrderModel>(It.IsAny<OrderRequest>()), Times.Once);
            _mapper.Verify(x => x.Map<ReceiptResponse>(It.IsAny<ReceiptModel>()), Times.Once);
            _orderService.Verify(x => x.ProcessOrder(It.IsAny<IOrder>()), Times.Once);

        }

        public void Dispose()
        {
            _mapper.VerifyAll();
            _orderService.VerifyAll();
        }
    }
}
