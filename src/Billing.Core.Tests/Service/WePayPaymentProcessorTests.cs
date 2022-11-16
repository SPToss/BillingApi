using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.PaymentProcessors;

namespace Billing.Core.Tests.Service
{
    public class WePayPaymentProcessorTests : IDisposable
    {
        private readonly TestObjects _testObjects;
        private readonly IPaymentProcessor _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDiscountFactory> _discountFactoryMock;
        private readonly Mock<IDiscountCalculator> _discountCalculatorMock;

        public WePayPaymentProcessorTests()
        {
            _testObjects = new TestObjects();
            _mapperMock = new Mock<IMapper>();
            _discountFactoryMock = new Mock<IDiscountFactory>();
            _discountCalculatorMock = new Mock<IDiscountCalculator>();
            _sut = new WePayPaymentProcessor(_discountFactoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ProcessOrder_WhenCorrectOrder_ShouldProcessOrder()
        {
            //Arrange
            var oder = _testObjects.GetOrderModel(10, 2);
            var payment = _testObjects.GetPaymentModel(oder);

            _mapperMock.Setup(x => x.Map<PaymentModel>(It.IsAny<IOrder>())).Returns(payment);
            _discountFactoryMock.Setup(x => x.Create(It.IsAny<IOrder>())).Returns(_discountCalculatorMock.Object);
            _discountCalculatorMock.Setup(x => x.CalculatePriceAfterDiscount(It.IsAny<IOrder>())).ReturnsAsync(20);

            //Asc
            var result = await _sut.ProcessOrder(oder);

            //Assert
            result.Should().NotBeNull();

            _mapperMock.Verify(x => x.Map<PaymentModel>(It.IsAny<IOrder>()), Times.Once);
            _discountCalculatorMock.Verify(x => x.CalculatePriceAfterDiscount(It.IsAny<IOrder>()), Times.Once);
            _discountFactoryMock.Verify(x => x.Create(It.IsAny<IOrder>()), Times.Once());
        }

        public void Dispose()
        {

            _mapperMock.VerifyAll();
            _discountCalculatorMock.VerifyAll();
            _discountFactoryMock.VerifyAll();
        }
    }
}
