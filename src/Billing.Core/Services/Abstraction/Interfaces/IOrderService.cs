﻿using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Services.Abstraction.Interfaces
{
    public interface IOrderService
    {
        Task<IReceipt> ProcessOrder(IOrder order);
    }
}
