using System.ComponentModel.DataAnnotations;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Blog
{
    public int BlogId { get; set; }
    public int UserId { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    public List<Post> Posts { get; set; } = [];
    public User User { get; set; } = null!;
}
