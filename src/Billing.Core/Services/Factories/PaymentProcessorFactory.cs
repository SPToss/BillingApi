using XYZ.Billing.Core.Models.Interfaces;
using XYZ.Billing.Core.Models.Types;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.PaymentProcessors;

namespace XYZ.Billing.Core.Services.Factories
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProcessor Create(IOrder order) => order.PaymentGateway switch
        {
            (int)PaymentGatewayType.DefaultGateway => (IPaymentProcessor)_serviceProvider.GetService(typeof(DefaultPaymentProcessor))!,
            (int)PaymentGatewayType.ApplePay => (IPaymentProcessor)_serviceProvider.GetService(typeof(ApplePayPaymentProcessor))!,
            (int)PaymentGatewayType.GooglePay => (IPaymentProcessor)_serviceProvider.GetService(typeof(GooglePayPaymentProcessor))!,
            (int)PaymentGatewayType.WePay => (IPaymentProcessor)_serviceProvider.GetService(typeof(WePayPaymentProcessor))!,
            _ => (IPaymentProcessor)_serviceProvider.GetService(typeof(DefaultPaymentProcessor))!
        };
    }
}
