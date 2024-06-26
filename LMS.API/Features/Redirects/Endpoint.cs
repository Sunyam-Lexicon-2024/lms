namespace Redirects;

public class Endpoint : EndpointWithoutRequest
{

    public override void Configure()
    {
        RoutePrefixOverride(string.Empty);
        Get("/");
        Description(ep => ep.ExcludeFromDescription());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendRedirectAsync("/swagger");
    }

}