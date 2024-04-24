using System.ComponentModel.DataAnnotations;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Post
{
    public int PostId { get; set; }
    public int BlogId { get; set; }

    [MaxLength(100)]
    public required string Title { get; set; }
    public required string Content { get; set; }
    public Blog Blog { get; set; } = null!;
    public List<Comment> Comments { get; set; } = [];
}
