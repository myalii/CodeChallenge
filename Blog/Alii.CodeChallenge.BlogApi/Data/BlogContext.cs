using Alii.CodeChallenge.BlogApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Alii.CodeChallenge.BlogApi.Data;

public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ensure that the optionsBuilder is only configured if it's not already configured
        // This is useful to prevent overriding options when they are passed through the constructor
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=blog.db");
        }
    }
}
