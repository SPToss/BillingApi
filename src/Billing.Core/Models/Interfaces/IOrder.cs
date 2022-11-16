namespace XYZ.Billing.Core.Models.Interfaces
{
    public interface IOrder
    {
        Guid Number { get; set; }

        Guid UserId { get; set; }

        decimal Amount { get; set; }

        int PaymentGateway { get; set; }

        string? Description { get; set; }
    }
}
