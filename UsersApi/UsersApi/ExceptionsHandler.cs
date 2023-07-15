using Newtonsoft.Json;
using System.Net;

namespace UsersApi
{
    public class ExceptionsHandler
    {
        private readonly ILogger<ExceptionsHandler> _logger;
        private readonly RequestDelegate _next;

        public ExceptionsHandler(RequestDelegate next, ILogger<ExceptionsHandler> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                await HandleExceptionAsync(httpContext);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = "Something went wrong"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);

        }
    }
}
