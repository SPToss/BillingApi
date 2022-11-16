namespace XYZ.Billing.Core.Models.Exceptions
{
    public class OrderValidationException : ValidationException

    {
        public OrderValidationException(string message) : base(message)
        {
        }
    }
}
