namespace Alii.CodeChallenge.BlogApi.Data;

public static class SeedDataExtension
{
    public static void SeedDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BlogContext>();
            
            SeedData.Initialize(context);
        }
    }
}
