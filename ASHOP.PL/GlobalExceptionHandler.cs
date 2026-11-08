using ASHOP.DAL.DTO;
using Microsoft.AspNetCore.Diagnostics;

namespace ASHOP.PL
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var result = new Result<object>
            {
                Success = false,
                Message = exception.InnerException?.Message ?? exception.Message
            };

            httpContext.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                result,
                cancellationToken
            );

            return true;
        }
    }
}