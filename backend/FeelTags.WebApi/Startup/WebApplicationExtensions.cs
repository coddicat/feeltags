namespace FeelTags.WebApi.Startup
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigureWebApplication(this WebApplication webApplication)
        {
            if (webApplication.Environment.IsDevelopment())
            {
                webApplication
                    .UseCors(options => options
                        .WithOrigins("http://127.0.0.1", "http://192.168.1.19")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());

                webApplication
                    .UseSwagger()
                    .UseSwaggerUI();
            }
            else
            {
                webApplication.UseHsts();
            }

            //webApplication
            //    .UseAuthentication()
            //    .UseAuthorization();

            //webApplication.Lifetime.ApplicationStarted.Register(() => ApplicationStarted(webApplication));

            return webApplication;
        }
    }
}
