using Alii.CodeChallenge.BlogApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Alii.CodeChallenge.BlogApi.Data;

public static class SeedData
{
    public static void Initialize(BlogContext context, PasswordHasher<User> passwordHasher)
    {
        context.Database.EnsureCreated();

        // Check if the database is already seeded
        if (context.Users.Any())
        {
            return;
        }

        // Seed users, passwords will be hashed
        var users = new[]
        {
            new User { Name = "Alice", PasswordHash = "AlicePassword" },
            new User { Name = "Bob", PasswordHash = "BobPassword" },
            new User { Name = "Charlie", PasswordHash = "CharliePassword" }
        };

        foreach (var user in users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
        }

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
            new Post("First Post", "Hello World!") { Blog = blogs[0] },
            new Post("Second Post", "The quick brown fox...") { Blog = blogs[1] }
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
