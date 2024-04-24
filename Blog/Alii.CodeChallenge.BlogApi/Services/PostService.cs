using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Microsoft.EntityFrameworkCore;

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

        var post = new Post
        {
            BlogId = postCreateDto.BlogId,
            Title = postCreateDto.Title,
            Content = postCreateDto.Content
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

    public async Task<List<PostSummaryDto>> GetPostsSummaryForUserAsync(int userId)
    {
        // TODO: Implement this method
        logger.LogError("GetPostsSummaryForUserAsync is not implemented");
        throw new NotImplementedException();
    }

    public async Task<Post> UpdatePostAsync(int userId, int postId, PostEditDto postEditDto)
    {
        // TODO: Implement this method  
        logger.LogError("UpdatePostAsync is not implemented");
        throw new NotImplementedException();
    }
}
