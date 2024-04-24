using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace Alii.CodeChallenge.BlogApi.Services;

public class BlogService(ILogger<BlogService> logger, BlogContext blogContext)
{
    public async Task<List<BlogDto>> GetAllBlogsAsync()
    {
        var blogs = await blogContext.Blogs
            .AsNoTracking()
            .Select(x => new BlogDto
            {
                BlogId = x.BlogId,
                Name = x.Name,
                Author = x.User.Name,
            })
            .ToListAsync();

        return blogs;
    }
}
