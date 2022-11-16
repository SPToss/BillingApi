using FluentAssertions;
using Moq;
using Xunit;
using XYZ.Billing.Core.Models.Types;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.Factories;
using XYZ.Billing.Core.Services.PaymentProcessors;

namespace Billing.Core.Tests.Service
{
    public class PaymentProcessorFactoryTests : IDisposable
    {
        private readonly TestObjects _testObjects;
        private readonly IPaymentProcessorFactory _sut;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly Mock<IPaymentProcessor> _defaultPaymentProcessorMock;
        private readonly Mock<IPaymentProcessor> _applePaymentProcessorMock;
        private readonly Mock<IPaymentProcessor> _googlePaymentProcessorMock;
        private readonly Mock<IPaymentProcessor> _wePayPaymentProcessorMock;

        public PaymentProcessorFactoryTests()
        {
            _testObjects = new TestObjects();
            _applePaymentProcessorMock = new Mock<IPaymentProcessor>();
            _defaultPaymentProcessorMock = new Mock<IPaymentProcessor>();
            _googlePaymentProcessorMock = new Mock<IPaymentProcessor>();
            _wePayPaymentProcessorMock = new Mock<IPaymentProcessor>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _sut = new PaymentProcessorFactory(_serviceProviderMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenDefaultProcessorIsPassed_ShouldReturnDefaultProcessor()
        {
            var order = _testObjects.GetOrderModel(10, (int)PaymentGatewayType.DefaultGateway);


            _serviceProviderMock.Setup(x => x.GetService(typeof(DefaultPaymentProcessor))).Returns(_defaultPaymentProcessorMock.Object);

            //Act
            var processor = _sut.Create(order);

            //Assert
            processor.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultPaymentProcessor)), Times.Once);
            _serviceProviderMock.Verify(x => x.GetService(typeof(GooglePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(ApplePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(WePayPaymentProcessor)), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenGooglePaymentProcessorIsPassed_ShouldReturnGoogleProcessor()
        {
            var order = _testObjects.GetOrderModel(10, (int)PaymentGatewayType.GooglePay);

            _serviceProviderMock.Setup(x => x.GetService(typeof(GooglePayPaymentProcessor))).Returns(_defaultPaymentProcessorMock.Object);

            //Act
            var processor = _sut.Create(order);

            //Assert
            processor.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(GooglePayPaymentProcessor)), Times.Once);
            _serviceProviderMock.Verify(x => x.GetService(typeof(ApplePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(WePayPaymentProcessor)), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenAppleIsPassed_ShouldReturnAppleProcessor()
        {
            var order = _testObjects.GetOrderModel(10, (int)PaymentGatewayType.ApplePay);


            _serviceProviderMock.Setup(x => x.GetService(typeof(ApplePayPaymentProcessor))).Returns(_defaultPaymentProcessorMock.Object);

            //Act
            var processor = _sut.Create(order);

            //Assert
            processor.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(GooglePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(ApplePayPaymentProcessor)), Times.Once);
            _serviceProviderMock.Verify(x => x.GetService(typeof(WePayPaymentProcessor)), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenWePayPaymentProcessorPassed_ShouldReturnWePayProcessor()
        {
            var order = _testObjects.GetOrderModel(10, (int)PaymentGatewayType.WePay);


            _serviceProviderMock.Setup(x => x.GetService(typeof(WePayPaymentProcessor))).Returns(_defaultPaymentProcessorMock.Object);

            //Act
            var processor = _sut.Create(order);

            //Assert
            processor.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(GooglePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(ApplePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(WePayPaymentProcessor)), Times.Once);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenNonExistingPaymentProcessorPassed_ShouldReturnDefaultProcessor()
        {
            var order = _testObjects.GetOrderModel(10, 20);


            _serviceProviderMock.Setup(x => x.GetService(typeof(DefaultPaymentProcessor))).Returns(_defaultPaymentProcessorMock.Object);

            //Act
            var processor = _sut.Create(order);

            //Assert
            processor.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultPaymentProcessor)), Times.Once);
            _serviceProviderMock.Verify(x => x.GetService(typeof(GooglePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(ApplePayPaymentProcessor)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(WePayPaymentProcessor)), Times.Never);
        }

        public void Dispose()
        {
            _applePaymentProcessorMock.VerifyAll();
            _defaultPaymentProcessorMock.VerifyAll();
            _googlePaymentProcessorMock.VerifyAll();
            _wePayPaymentProcessorMock.VerifyAll();
            _serviceProviderMock.VerifyAll();
        }
    }
}
