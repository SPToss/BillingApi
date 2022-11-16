namespace XYZ.Billing.Core.Models.Exceptions
{
    public class PaymentValidationException : ValidationException
    {
        public PaymentValidationException(string message) : base(message)
        {
        }
    }
}
