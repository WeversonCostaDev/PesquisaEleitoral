using PesquisaEleitoral_v2.DTOs;

namespace PesquisaEleitoral_v2.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                await ProcessException(ex, context);
            }
        }
        private async Task ProcessException(Exception ex, HttpContext context)
        {
            var statusCode = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                new ErrorDetails 
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                }.ToString());

        }
    }
}
