using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IDiscountCalculator
    {
        Task<decimal> CalculatePriceAfterDiscount(IOrder order);
    }
}
