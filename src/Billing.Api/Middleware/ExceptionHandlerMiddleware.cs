using XYZ.Billing.Core.Models;
using XYZ.Billing.Core.Models.Exceptions;
using XYZ.Billing.Core.Models.Response;

namespace XYZ.Billing.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ErrorResponse? errorResponse = null;

            try
            {
                await _next(context);
            }
            catch (PaymentValidationException exception)
            {
                errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    ErrorMessage = $"An Error during payment processing : {exception.Message}"
                };
            }
            catch (OrderValidationException exception)
            {
                errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    ErrorMessage = $"Invalid order : {exception.Message}"
                };
            }
            catch (ValidationException exception)
            {
                errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    ErrorMessage = $"An validation error occurred : {exception.Message}"
                };
            }
            catch (Exception exception)
            {
                errorResponse = new ErrorResponse
                {
                    ErrorCode = 500,
                    ErrorMessage = $"An error occurred : {exception.Message}"
                };
            }
            finally
            {
                if (errorResponse != null)
                {
                    context.Response.StatusCode = errorResponse.ErrorCode;
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
        }
    }
}
