using System.ComponentModel.DataAnnotations;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Comment
{
    public int CommentId { get; set; }
    public int PostId { get; set; }

    [MaxLength(100)]
    public required string Author { get; set; }
    
    [MaxLength(200)]
    public required string Content { get; set; }
    public Post Post { get; set; } = null!;
}
