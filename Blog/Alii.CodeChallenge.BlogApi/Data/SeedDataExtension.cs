using Alii.CodeChallenge.BlogApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Alii.CodeChallenge.BlogApi.Data;

public static class SeedDataExtension
{
    public static void SeedDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BlogContext>();
            var passwordHasher = services.GetRequiredService<PasswordHasher<User>>();

            SeedData.Initialize(context, passwordHasher);
        }
    }
}
