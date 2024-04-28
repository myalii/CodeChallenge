using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Post
{
    public int PostId { get; set; }
    public int BlogId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; } = Array.Empty<byte>();

    [Required]
    public Blog Blog { get; set; } = null!;
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Post(string title, string content)
    {
        Title = title;
        Content = content;
    }
}
