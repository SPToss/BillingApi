using FluentAssertions;
using Moq;
using Xunit;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.DiscountCalculators;
using XYZ.Billing.Core.Services.Factories;

namespace Billing.Core.Tests.Service
{
    public class DiscountProviderTests : IDisposable
    {
        private readonly TestObjects _testObjects;
        private readonly IDiscountFactory _sut;
        private readonly Mock<IDiscountCalculator> _bulkCalculatorMock;
        private readonly Mock<IDiscountCalculator> _defaultCalculatorMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;

        public DiscountProviderTests()
        {
            _testObjects = new TestObjects();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _bulkCalculatorMock = new Mock<IDiscountCalculator>();
            _defaultCalculatorMock = new Mock<IDiscountCalculator>();
            _sut = new DiscountFactory(_serviceProviderMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenAmountGraterThanThreshold_ShouldReturnBulkProcessor()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(100_000_001, 1);

            _serviceProviderMock.Setup(x => x.GetService(typeof(BulkDiscountCalculator))).Returns(_bulkCalculatorMock.Object);

            //Act
            var calculator = _sut.Create(order);

            //Assert
            calculator.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(BulkDiscountCalculator)), Times.Once);
            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultDiscountCalculator)), Times.Never);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Create_WhenAmountLesserThanThreshold_ShouldReturnBulkProcessor()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(90_000_00, 1);

            _serviceProviderMock.Setup(x => x.GetService(typeof(DefaultDiscountCalculator))).Returns(_defaultCalculatorMock.Object);

            //Act
            var calculator = _sut.Create(order);

            //Assert
            calculator.Should().NotBeNull();

            _serviceProviderMock.Verify(x => x.GetService(typeof(BulkDiscountCalculator)), Times.Never);
            _serviceProviderMock.Verify(x => x.GetService(typeof(DefaultDiscountCalculator)), Times.Once);
        }

        public void Dispose()
        {
            _serviceProviderMock.VerifyAll();
            _bulkCalculatorMock.VerifyAll();
            _defaultCalculatorMock.VerifyAll();
        }
    }
}
