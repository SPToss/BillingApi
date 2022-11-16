using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using XYZ.Billing.Core.Models.Mapping;
using XYZ.Billing.Core.Services;
using XYZ.Billing.Core.Services.Abstraction.Interfaces;
using XYZ.Billing.Core.Services.DiscountCalculators;
using XYZ.Billing.Core.Services.Factories;
using XYZ.Billing.Core.Services.PaymentProcessors;
using XYZ.Billing.Core.Services.Validation;

namespace XYZ.Billing.Core.Extension
{
    public static class BillingCoreServiceCollectionsExtensions
    {
        public static IServiceCollection AddCoreCollections(this IServiceCollection services)
        {
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IDiscountFactory, DiscountFactory>();
            services.AddScoped<IOrderValidator, OrderValidator>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<DefaultPaymentProcessor>()
                .AddScoped<IPaymentProcessor, DefaultPaymentProcessor>(s => s.GetService<DefaultPaymentProcessor>()!);
            services.AddScoped<ApplePayPaymentProcessor>()
                .AddScoped<IPaymentProcessor, ApplePayPaymentProcessor>(s => s.GetService<ApplePayPaymentProcessor>()!);
            services.AddScoped<GooglePayPaymentProcessor>()
                .AddScoped<IPaymentProcessor, GooglePayPaymentProcessor>(
                    s => s.GetService<GooglePayPaymentProcessor>()!);
            services.AddScoped<WePayPaymentProcessor>()
                .AddScoped<IPaymentProcessor, WePayPaymentProcessor>(s => s.GetService<WePayPaymentProcessor>()!);

            services.AddScoped<BulkDiscountCalculator>()
                .AddScoped<IDiscountCalculator, BulkDiscountCalculator>(s => s.GetService<BulkDiscountCalculator>()!);
            services.AddScoped<DefaultDiscountCalculator>()
                .AddScoped<IDiscountCalculator, DefaultDiscountCalculator>(s =>
                    s.GetService<DefaultDiscountCalculator>()!);

            services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            return services;
        }
    }
}
