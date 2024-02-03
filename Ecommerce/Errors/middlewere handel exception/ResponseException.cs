using System.Net;
using System.Text.Json;

namespace Ecommerce.Errors.middlewere_handel_exception
{
    public class ResponseException
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ResponseException> logger;
        private readonly IHostEnvironment env;

        public ResponseException(RequestDelegate next,ILogger<ResponseException> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }catch (Exception ex)
            {
                // log error for developer
                logger.LogError(ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode= (int) HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment() ? new ApiResponseException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) :
                    new ApiResponseException((int)HttpStatusCode.InternalServerError);
                var json=JsonSerializer.Serialize(response);
                context.Response.WriteAsync(json);
            }
        }
    }
}
