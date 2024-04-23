using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Alii.CodeChallenge.BlogApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace Alii.CodeChallenge.BlogApi.Tests.Services;

public class PostSummaryServiceTests
{
    private DbContextOptions<BlogContext> _options;

    public PostSummaryServiceTests()
    {
        // Configure the in-memory database
        _options = new DbContextOptionsBuilder<BlogContext>()
            .UseInMemoryDatabase(databaseName: $"BlogContextTestDatabase_{Guid.NewGuid()}")
            .Options;

        // Seed the database
        SeedDatabase();
    }

    [Fact]
    public async Task GetPostsSummaryForUserAsync_ReturnsCorrectData()
    {
        // Arrange  
        var userId = 1;
        var expectedSummaries = new List<PostSummaryDto>
        {
            new() { PostId = 1, Title = "Test Post 1", CommentCount = 2 },
            new() { PostId = 3, Title = "Test Post 3", CommentCount = 1 }
        };

        var mockLogger = new Mock<ILogger<PostSummaryService>>();
        using var blogContext = new BlogContext(_options);
        var postSummaryService = new PostSummaryService(mockLogger.Object, blogContext);

        // Act  
        var actualSummaries = await postSummaryService.GetPostsSummaryForUserAsync(userId);

        // Assert  
        Assert.NotNull(actualSummaries);
        Assert.Equal(expectedSummaries.Count, actualSummaries.Count);
        foreach (var expectedSummary in expectedSummaries)
        {
            var actualSummary = actualSummaries.FirstOrDefault(summary => summary.PostId == expectedSummary.PostId);
            Assert.NotNull(actualSummary);
            Assert.Equal(expectedSummary.Title, actualSummary.Title);
            Assert.Equal(expectedSummary.CommentCount, actualSummary.CommentCount);
        }
    }

    private void SeedDatabase()
    {
        using var context = new BlogContext(_options);
        var user = new User { UserId = 1, Name = "Test User" };
        var blog = new Blog { BlogId = 1, Name = "Test Blog", UserId = user.UserId };
        user.Blogs = [blog];

        var posts = new List<Post>
            {  
                // Posts with comments
                new() {
                    PostId = 1,
                    BlogId = blog.BlogId,
                    Title = "Test Post 1",
                    Content = "Test Content 1",
                    Blog = blog,
                    Comments =
                    [
                        new Comment { CommentId = 1, PostId = 1, Author = "Test Author 1", Content = "Test Comment 1" },
                        new Comment { CommentId = 2, PostId = 1, Author = "Test Author 2", Content = "Test Comment 2" }
                    ]
                },  
                // Post without comments
                new() {
                    PostId = 2,
                    BlogId = blog.BlogId,
                    Title = "Test Post 2",
                    Content = "Test Content 2",
                    Blog = blog,
                    Comments = []
                },  
                // Another post with comments
                new() {
                    PostId = 3,
                    BlogId = blog.BlogId,
                    Title = "Test Post 3",
                    Content = "Test Content 3",
                    Blog = blog,
                    Comments =
                    [
                        new Comment { CommentId = 3, PostId = 3, Author = "Test Author 3", Content = "Test Comment 3" }
                    ]
                }
            };

        context.Users.Add(user);
        context.Blogs.Add(blog);
        context.Posts.AddRange(posts);
        context.SaveChanges();
    }
}
