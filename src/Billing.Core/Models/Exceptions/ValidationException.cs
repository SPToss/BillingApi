namespace XYZ.Billing.Core.Models.Exceptions
{
    public abstract class ValidationException : Exception
    {
        protected ValidationException(string message)
            : base(message)
        {
        }
    }
}
