using XYZ.Billing.Core.Models.Interfaces;

namespace XYZ.Billing.Core.Models
{
    public class PaymentModel : OrderModel, IPayment
    {
        public decimal FinalAmount { get; set; }
    }
}
