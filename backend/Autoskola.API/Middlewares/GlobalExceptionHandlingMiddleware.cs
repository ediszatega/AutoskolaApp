using Autoskola.Core.Models;

namespace Autoskola.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
            => this.logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, ex.InnerException);

                if (ex is HttpException)
                {
                    HttpException _ex = (HttpException)ex;
                    context.Response.StatusCode = _ex.StatusCode;
                }
                else
                {
                    context.Response.StatusCode = 500; 
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
        }
    }
}
