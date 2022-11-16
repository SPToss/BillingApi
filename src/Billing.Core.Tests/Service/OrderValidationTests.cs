using FluentAssertions;
using Xunit;
using XYZ.Billing.Core.Models.Exceptions;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.Validation;

namespace Billing.Core.Tests.Service
{
    public class OrderValidationTests
    {
        private readonly TestObjects _testObjects;
        private readonly IOrderValidator _sut;

        public OrderValidationTests()
        {
            _testObjects = new TestObjects();
            _sut = new OrderValidator();
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task Validate_WhenOrderIsEmpty_ShouldThrowOrderValidationException()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(10, 1);
            order.Number = Guid.Empty;

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.Validate(order));
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task Validate_WhenUserIsEmpty_ShouldThrowOrderValidationException()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(10, 1);
            order.UserId = Guid.Empty;

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.Validate(order));
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task Validate_WhenAmountIsNegative_ShouldThrowOrderValidationException()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(-10, 1);

            //Act
            await Assert.ThrowsAsync<OrderValidationException>(async () => await _sut.Validate(order));
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void Validate_OrderIsValid_ShouldReturnCompletedTask()
        {
            //Arrange
            var order = _testObjects.GetOrderModel(10, 1);

            //Act
            var result =  _sut.Validate(order);

            //Assert
            result.Should().Be(Task.CompletedTask);
        }
    }
}
