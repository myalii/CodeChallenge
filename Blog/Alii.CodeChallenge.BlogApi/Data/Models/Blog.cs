using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Blog
{
    public int BlogId { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    public List<Post> Posts { get; set; } = [];
    public User User { get; set; } = null!;
}
