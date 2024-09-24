using System.Net;
using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleWare(RequestDelegate Next,ILogger<ExceptionMiddleWare> Logger,IHostEnvironment environment)
        {
            next = Next;
            logger = Logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var Response = environment.IsDevelopment()? new ApiExcceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExcceptionResponse(500);
                var options = new JsonSerializerOptions(){
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response, options);
                httpContext.Response.WriteAsync(JsonResponse);

            }

        }
    }
}
