using System.Net;
using System.Text.Json;

namespace AspCoreWebApi.Middleware
{
    public class ExceptionMiddlewarecs
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewarecs> _logger;

        public ExceptionMiddlewarecs(RequestDelegate next,ILogger<ExceptionMiddlewarecs> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception occurred. Method: {Method}, Path: {Path}",
                    context.Request.Method,
                    context.Request.Path);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    statusCode = 500,
                    message = "Something went wrong."
                });
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,Exception exception) 
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType= "application/json";

            var response = new
            {
                StatusCodes = context.Response.StatusCode,
                message = "an unexpected error occured"
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
