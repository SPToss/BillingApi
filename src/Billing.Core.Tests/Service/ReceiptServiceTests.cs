
using FluentAssertions;
using Xunit;
using XYZ.Billing.Core.Services;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;

namespace Billing.Core.Tests.Service
{
    public class ReceiptServiceTests
    {
        private readonly TestObjects _testObjects;
        private readonly IReceiptService _sut;
        public ReceiptServiceTests()
        {
            _testObjects = new TestObjects();
            _sut = new ReceiptService();
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public void GenerateReceipt_WhenCalled_ShouldGenerateReceipt()
        {
            //Arrange 
            var payment = _testObjects.GetPaymentModel(_testObjects.GetOrderModel(10, 2));
            
            //Act
            var result = _sut.GenerateReceipt(payment);

            //Arrange

            result.Should().NotBeNull();
        }
    }
}
