namespace XYZ.Billing.Core.Models.Interfaces
{
    public interface IPayment : IOrder
    {
        decimal FinalAmount { get; set; }
    }
}
