namespace LMS.API.Extensions;

public static class WebAppExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseDefaultExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseFastEndpoints(config =>
            {
                config.Endpoints.RoutePrefix = "api"; // prefix all routes with "/api"
                config.Endpoints.Configurator = ep =>
                {
                    ep.AllowAnonymous(); // disable auth temporarily
                };
            })
            .UseSwaggerGen();
        }
        else
        {
            app.UseFastEndpoints(config =>
            {
                config.Endpoints.RoutePrefix = "api"; // prefix all routes with "/api"
            });
        }

        return app;
    }
}