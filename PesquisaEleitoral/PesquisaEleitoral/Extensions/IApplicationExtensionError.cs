using PesquisaEleitoral.Middlewares;

namespace PesquisaEleitoral.Extensions
{
    public static class IApplicationExtensionError
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
