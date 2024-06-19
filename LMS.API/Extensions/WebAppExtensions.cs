using FastEndpoints;

namespace LMS.API.Extensions;

public static class WebAppExtensions
{

    public static IApplicationBuilder ConfigureApplication(this IApplicationBuilder app)
    {
        app.UseDefaultExceptionHandler()
           .UseFastEndpoints();

        return app;
    }
}