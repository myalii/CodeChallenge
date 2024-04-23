using Alii.CodeChallenge.BlogApi.Data.Models;

namespace Alii.CodeChallenge.BlogApi.Data;

public static class SeedData
{
    public static void Initialize(BlogContext context)
    {
        context.Database.EnsureCreated();

        // Check if the database is already seeded  
        if (context.Users.Any())
        {
            return;
        }

        // Seed users  
        var users = new[]
        {
            new User { Name = "Alice" },
            new User { Name = "Bob" },
            new User { Name = "Charlie" }
        };

        context.Users.AddRange(users);

        // Seed blogs  
        var blogs = new[]
        {
            new Blog { Name = "Alice's Blog", User = users[0] },
            new Blog { Name = "Bob's Blog", User = users[1] }
        };

        context.Blogs.AddRange(blogs);

        // Seed posts  
        var posts = new[]
        {
            new Post { Title = "First Post", Content = "Hello World!", Blog = blogs[0] },
            new Post { Title = "Second Post", Content = "The quick brown fox...", Blog = blogs[1] }
        };

        context.Posts.AddRange(posts);

        // Seed comments  
        var comments = new[]
        {
            new Comment { Author = "Dave", Content = "Nice post!", Post = posts[0] },
            new Comment { Author = "Eve", Content = "Interesting read.", Post = posts[1] }
        };

        context.Comments.AddRange(comments);

        // Save changes to the database  
        context.SaveChanges();
    }
}
