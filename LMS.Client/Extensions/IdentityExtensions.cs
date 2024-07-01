namespace LMS.Client.Extensions;

public static class IdentityExtensions
{
    public static async Task<WebApplication> SeedIdentityRoles(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;

        var roles = app.Configuration.GetSection("Identity:Roles").Get<List<string>>();

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var r in roles!)
        {
            var roleExist = await roleManager.RoleExistsAsync(r);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(r));
            }
        }

        var superUser = new User
        {
            Name = app.Configuration["Identity:SuperUser:Name"]!,
            UserName = app.Configuration["Identity:SuperUser:Email"],
            Email = app.Configuration["Identity:SuperUser:Email"],
            EmailConfirmed = true,
        };

        var userExists = await userManager.FindByEmailAsync(superUser.Email!);

        if (userExists is null)
        {
            var createPowerUser = await userManager.CreateAsync(superUser, app.Configuration["Identity:SuperUser:Password"]!);
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(superUser, "Admin");
            }
        }

        return app;
    }
}