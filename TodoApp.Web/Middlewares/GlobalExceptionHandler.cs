using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Web.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                                    Exception exception,
                                                    CancellationToken cancellationToken)
        {
            logger.LogError(exception, "例外が発生しました: {Message}", exception.Message);

            // バリデーションの例外の場合は 400 Bad Request を返す
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "入力内容に不備があります。",
                    Extensions =
                    {
                        ["error"] = validationException.Errors.Select(e => e.ErrorMessage)
                    }
                };

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            // それ以外の予期せぬエラーは 500 Internal Server Error
            return false;
        }
    }
}
