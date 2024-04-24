using System.ComponentModel.DataAnnotations;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class User
{
    public int UserId { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public required string PasswordHash { get; set; }
    public List<Blog> Blogs { get; set; } = [];
}
