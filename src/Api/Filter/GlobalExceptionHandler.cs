using Application.Common;
using Application.Models.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Filter
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponse = new ErrorResponse();
            int statusCode = StatusCodes.Status500InternalServerError;
            if (exception is BussinessException ex)
            {
                errorResponse.ErrorCode = ex.ErrorCode;
                errorResponse.Message = ex.Message;
                statusCode = ex.StatusCode;
            }else
            {
                errorResponse.ErrorCode = ErrorCode.SomethingWentWrong;
                errorResponse.Message = exception.Message;
                statusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError("Error {message}", exception.Message);
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
