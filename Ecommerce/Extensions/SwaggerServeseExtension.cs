namespace Ecommerce.Extenstions
{
    public static class SwaggerServicesExtension
    {
        public static WebApplication AddSwaggerServicesMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
