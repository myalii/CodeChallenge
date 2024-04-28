using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Microsoft.EntityFrameworkCore;
using Alii.CodeChallenge.BlogApi.Utilities;

namespace Alii.CodeChallenge.BlogApi.Services;

public class PostService(ILogger<PostService> logger, BlogContext blogContext)
{
    public async Task<PostDto?> GetPostAsync(int postId)
    {
        var post = await blogContext.Posts
            .AsNoTracking()
            .Select(p => new PostDto
            {
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content,
                Comments = p.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Author = c.Author,
                    Content = c.Content
                }).ToList()
            })
            .FirstOrDefaultAsync(p => p.PostId == postId);

        return post;
    }

    public async Task<PostDto> CreatePostAsync(int userId, PostCreateDto postCreateDto)
    {
        var blog = await blogContext.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BlogId == postCreateDto.BlogId && b.UserId == userId);
        if (blog == null)
        {
            throw new InvalidOperationException("Blog not found, or user is not authorized to create posts in this blog.");
        }

        var post = new Post(postCreateDto.Title, postCreateDto.Content)
        {
            BlogId = postCreateDto.BlogId
        };


        blogContext.Posts.Add(post);
        await blogContext.SaveChangesAsync();

        return new PostDto
        {
            PostId = post.PostId,
            Title = post.Title,
            Content = post.Content
        };
    }

    public async Task<Result<List<PostSummaryDto>>> GetPostsSummaryForUserAsync(int userId)
    {
        if (userId <= 0)
        {
            return new Result<List<PostSummaryDto>>
            {
                IsSuccess = false,
                Message = "Invalid user ID provided.",
                ErrorType = ResultTypeEnum.ArgumentValidationError
            };
        }

        try
        {
            var postSummaries = await blogContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Comments)
                .Where(post => post.Blog.UserId == userId && post.Comments.Any())
                .Select(post => new PostSummaryDto
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    CommentCount = post.Comments.Count
                })
                .ToListAsync();

            logger.LogInformation("Successfully retrieved {Count} post summaries for user ID {UserId}", postSummaries.Count, userId);

            return new Result<List<PostSummaryDto>>
            {
                IsSuccess = true,
                Data = postSummaries
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving post summaries for user ID {UserId}", userId);
            return new Result<List<PostSummaryDto>>
            {
                IsSuccess = false,
                Message = "An error occurred while retrieving post summaries.",
                ErrorType = ResultTypeEnum.InternalServerError
            };
        }
    }

    public async Task<Result<PostDto>> UpdatePostAsync(int userId, int postId, PostEditDto postEditDto)
    {
        if (postEditDto == null)
        {
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "Post data must not be null",
                ErrorType = ResultTypeEnum.ArgumentValidationError
            };
        }

        if (string.IsNullOrEmpty(postEditDto.Title) || string.IsNullOrEmpty(postEditDto.Content))
        {
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "Post must have both Title and Content",
                ErrorType = ResultTypeEnum.ArgumentValidationError
            };
        }

        var post = await blogContext.Posts.Include(p => p.Blog).FirstOrDefaultAsync(p => p.PostId == postId);
        if (post == null)
        {
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "Post not found",
                ErrorType = ResultTypeEnum.NotFoundError
            };
        }

        if (post.Blog.UserId != userId)
        {
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "Unauthorized access",
                ErrorType = ResultTypeEnum.UnauthorizedError
            };
        }

        post.Title = postEditDto.Title;
        post.Content = postEditDto.Content;


        try
        {
            await blogContext.SaveChangesAsync();

            logger.LogInformation("Successfully updated post ID {PostId} for user ID {UserId}", postId, userId);

            return new Result<PostDto>
            {
                IsSuccess = true,
                Data = new PostDto(post)
            };
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogError(ex, "Concurrency error while updating post ID {PostId} for user ID {UserId}", postId, userId);
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "Post update conflict detected.",
                ErrorType = ResultTypeEnum.ConflictError
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while updating post ID {PostId} for user ID {UserId}", postId, userId);
            return new Result<PostDto>
            {
                IsSuccess = false,
                Message = "An error occurred while updating a post.",
                ErrorType = ResultTypeEnum.InternalServerError
            };
        }
    }
}
