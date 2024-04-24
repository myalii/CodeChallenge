using System.Security.Claims;
using Alii.CodeChallenge.BlogApi.Dto;
using Alii.CodeChallenge.BlogApi.Extensions;
using Alii.CodeChallenge.BlogApi.Services;

namespace Alii.CodeChallenge.BlogApi.Endpoints;

public static class BlogEndpoints
{
    public static void AddBlogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/blogs");

        // Get a post
        group.MapGet("/", async (BlogService blogService) =>
        {
            try
            {
                var blogDtos = await blogService.GetAllBlogsAsync();
                return Results.Ok(blogDtos);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
    }
}
