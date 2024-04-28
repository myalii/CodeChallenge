using System.Security.Claims;
using Alii.CodeChallenge.BlogApi.Dto;
using Alii.CodeChallenge.BlogApi.Extensions;
using Alii.CodeChallenge.BlogApi.Services;
using Alii.CodeChallenge.BlogApi.Utilities;

namespace Alii.CodeChallenge.BlogApi.Endpoints;

public static class PostEndpoints
{
    public static void AddPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/posts");

        // Get a post
        group.MapGet("/{postId:int}", async (int postId, PostService postService) =>
        {
            try
            {
                var postDto = await postService.GetPostAsync(postId);
                if (postDto == null)
                {
                    return Results.NotFound("Post not found.");
                }
                return Results.Ok(postDto);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // Get posts summary for a user
        group.MapGet("/user/{userId:int}", async (int userId, PostService postService) =>
        {
            var result = await postService.GetPostsSummaryForUserAsync(userId);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ResultTypeEnum.ArgumentValidationError => Results.BadRequest(result.Message),
                    ResultTypeEnum.InternalServerError => Results.Problem(result.Message),
                    _ => Results.Problem("An unexpected error occurred.")
                };
            }

            return Results.Ok(result.Data);
        });


        // Create a post
        group.MapPost("/", async (PostService postService, PostCreateDto postCreateDto, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            if (!userId.HasValue)
            {
                return Results.Unauthorized();
            }

            try
            {
                var post = await postService.CreatePostAsync(userId.Value, postCreateDto);
                return Results.Created($"/api/posts/{post.PostId}", post);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }).RequireAuthorization();

        // Update a post
        group.MapPut("/{postId:int}", async (int postId, PostService postService, PostEditDto postEditDto, ClaimsPrincipal user) =>
        {
            var userId = user.GetUserId();
            if (!userId.HasValue)
            {
                return Results.Unauthorized();
            }

            try
            {
                var updatedPost = await postService.UpdatePostAsync(userId.Value, postId, postEditDto);
                return Results.Ok(updatedPost);
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }).RequireAuthorization();
    }
}
