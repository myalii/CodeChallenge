using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Alii.CodeChallenge.BlogApi.Services;
using Alii.CodeChallenge.BlogApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Alii.CodeChallenge.BlogApi.Tests.Services;

public class PostServiceTests
{
    private readonly DbContextOptions<BlogContext> _options;

    public PostServiceTests()
    {
        // Configure the in-memory database
        _options = new DbContextOptionsBuilder<BlogContext>()
            .UseInMemoryDatabase(databaseName: $"BlogContextTestDatabase_{Guid.NewGuid()}")
            .Options;

        SeedDatabase();
    }

    [Fact]
    public async Task GetPostsSummaryForUserAsync_ReturnsCorrectData()
    {
        // Arrange
        var userId = 1;
        var expectedSummaries = new List<PostSummaryDto>
    {
        new PostSummaryDto { PostId = 1, Title = "Test Post 1", CommentCount = 2 },
        new PostSummaryDto { PostId = 3, Title = "Test Post 3", CommentCount = 1 }
    };

        var mockLogger = new Mock<ILogger<PostService>>();
        using var blogContext = new BlogContext(_options);
        var postSummaryService = new PostService(mockLogger.Object, blogContext);

        // Act  
        var actualSummaries = await postSummaryService.GetPostsSummaryForUserAsync(userId);


        // Assert
        Assert.NotNull(actualSummaries);
        Assert.True(actualSummaries.IsSuccess);
        Assert.NotNull(actualSummaries.Data);
        Assert.Equal(expectedSummaries.Count, actualSummaries.Data.Count);
        foreach (var expectedSummary in expectedSummaries)
        {
            var actualSummary = actualSummaries.Data.FirstOrDefault(summary => summary.PostId == expectedSummary.PostId);
            Assert.NotNull(actualSummary);
            Assert.Equal(expectedSummary.Title, actualSummary.Title);
            Assert.Equal(expectedSummary.CommentCount, actualSummary.CommentCount);
        }
    }

    private void SeedDatabase()
    {
        using var context = new BlogContext(_options);
        var user = new User { UserId = 1, Name = "Test User", PasswordHash = "Test Password" };
        var blog = new Blog { BlogId = 1, Name = "Test Blog", UserId = user.UserId };
        user.Blogs = [blog];

        var posts = new List<Post>
            {  
                // Posts with comments
                new("Test Post 1", "Test Content 1") {
                    PostId = 1,
                    BlogId = blog.BlogId,
                    Blog = blog,
                    Comments =
                    [
                        new Comment { CommentId = 1, PostId = 1, Author = "Test Author 1", Content = "Test Comment 1" },
                        new Comment { CommentId = 2, PostId = 1, Author = "Test Author 2", Content = "Test Comment 2" }
                    ]
                },  
                // Post without comments
                new("Test Post 2", "Test Content 2") {
                    PostId = 2,
                    BlogId = blog.BlogId,
                    Blog = blog,
                    Comments = []
                },  
                // Another post with comments
                new("Test Post 3", "Test Content 3") {
                    PostId = 3,
                    BlogId = blog.BlogId,
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
