using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlogContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("BlogDatabase")));
builder.Services.AddScoped<PostSummaryService>();

var app = builder.Build();
app.SeedDatabase();

app.MapGet("/api/posts/user/{userId:int}", async (int userId, PostSummaryService postSummaryService) =>
{
    try
    {
        var summaries = await postSummaryService.GetPostsSummaryForUserAsync(userId);
        return Results.Ok(summaries);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
