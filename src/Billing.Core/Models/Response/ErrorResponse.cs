namespace XYZ.Billing.Core.Models.Response
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
