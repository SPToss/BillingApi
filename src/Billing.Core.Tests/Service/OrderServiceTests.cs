using FluentAssertions;
using Moq;
using Xunit;
using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Exceptions;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace Billing.Core.Tests.Service
{
    public class OrderServiceTests : IDisposable

    {
        private readonly TestObjects _testObjects;
        private readonly IOrderService _sut;
        private readonly Mock<IOrderValidator> _orderValidatorMock;
        private readonly Mock<IPaymentProcessorFactory> _paymentProcessorFactoryMock;
        private readonly Mock<IReceiptService> _receiptServiceMock;
        private readonly Mock<IPaymentProcessor> _paymentProcessorMock;

        public OrderServiceTests()
        {
            _testObjects = new TestObjects();
            _orderValidatorMock = new Mock<IOrderValidator>();
            _paymentProcessorFactoryMock = new Mock<IPaymentProcessorFactory>();
            _receiptServiceMock = new Mock<IReceiptService>();
            _paymentProcessorMock = new Mock<IPaymentProcessor>();
            _sut = new OrderService(_orderValidatorMock.Object, _paymentProcessorFactoryMock.Object, _receiptServiceMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task ProcessOrder_WhenExceptionDuringValidation_ShouldNotProcess()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(-10, 1);

            _orderValidatorMock.Setup(x => x.Validate(It.IsAny<IOrder>())).Throws(new OrderValidationException(""));

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.ProcessOrder(order));

            //Assert
            _orderValidatorMock.Verify(x => x.Validate(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorFactoryMock.Verify(x => x.Create(It.IsAny<IOrder>()), Times.Never);
            _paymentProcessorMock.Verify(x => x.ProcessOrder(It.IsAny<IOrder>()), Times.Never);
            _receiptServiceMock.Verify(x => x.GenerateReceipt(It.IsAny<IPayment>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task ProcessOrder_WhenExceptionDuringFactoryCreation_ShouldNotProcess()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(-10, 1);

            _orderValidatorMock.Setup(x => x.Validate(It.IsAny<IOrder>())).Returns(Task.CompletedTask);
            _paymentProcessorFactoryMock.Setup(x => x.Create(It.IsAny<IOrder>())).Throws(new OrderValidationException(""));

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.ProcessOrder(order));

            //Assert
            _orderValidatorMock.Verify(x => x.Validate(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorFactoryMock.Verify(x => x.Create(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorMock.Verify(x => x.ProcessOrder(It.IsAny<IOrder>()), Times.Never);
            _receiptServiceMock.Verify(x => x.GenerateReceipt(It.IsAny<IPayment>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task ProcessOrder_WhenExceptionDuringGenerateReceipt_ShouldNotProcess()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(-10, 1);

            _orderValidatorMock.Setup(x => x.Validate(It.IsAny<IOrder>())).Returns(Task.CompletedTask);
            _paymentProcessorFactoryMock.Setup(x => x.Create(It.IsAny<IOrder>())).Returns(_paymentProcessorMock.Object);
            _paymentProcessorMock.Setup(x => x.ProcessOrder(It.IsAny<IOrder>())).ReturnsAsync(new PaymentModel());
            _receiptServiceMock.Setup(x => x.GenerateReceipt(It.IsAny<IPayment>()))
                .Throws(new OrderValidationException(""));

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.ProcessOrder(order));

            //Assert
            _orderValidatorMock.Verify(x => x.Validate(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorFactoryMock.Verify(x => x.Create(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorMock.Verify(x => x.ProcessOrder(It.IsAny<IOrder>()), Times.Once);
            _receiptServiceMock.Verify(x => x.GenerateReceipt(It.IsAny<IPayment>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task ProcessOrder_WhenProcessOrder_ShouldReturnValidReceipt()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(-10, 1);
            var payment = _testObjects.GetPaymentModel(order);
            var receipt = _testObjects.GetReceiptModel(payment);

            _orderValidatorMock.Setup(x => x.Validate(It.IsAny<IOrder>())).Returns(Task.CompletedTask);
            _paymentProcessorFactoryMock.Setup(x => x.Create(It.IsAny<IOrder>())).Returns(_paymentProcessorMock.Object);
            _paymentProcessorMock.Setup(x => x.ProcessOrder(It.IsAny<IOrder>())).ReturnsAsync(payment);
            _receiptServiceMock.Setup(x => x.GenerateReceipt(It.IsAny<IPayment>())).ReturnsAsync(receipt);

            //Act
            var result = await _sut.ProcessOrder(order);

            //Assert
            result.Should().NotBeNull();
            result.Amount.Should().Be(result.Amount);
            result.CreatedDate.Should().Be(result.CreatedDate);
            result.ReceiptId.Should().Be(result.ReceiptId);

            _orderValidatorMock.Verify(x => x.Validate(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorFactoryMock.Verify(x => x.Create(It.IsAny<IOrder>()), Times.Once);
            _paymentProcessorMock.Verify(x => x.ProcessOrder(It.IsAny<IOrder>()), Times.Once);
            _receiptServiceMock.Verify(x => x.GenerateReceipt(It.IsAny<IPayment>()), Times.Once);
        }

        public void Dispose()
        {
            _orderValidatorMock.VerifyAll();
            _paymentProcessorFactoryMock.VerifyAll();
            _paymentProcessorMock.VerifyAll();
            _receiptServiceMock.VerifyAll();
        }
    }
}
