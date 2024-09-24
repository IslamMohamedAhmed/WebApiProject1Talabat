namespace Talabat.API.Extensions
{
    public static class AddSwaggerExtension
    {
        public static WebApplication AddSwaggerMiddleware(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
