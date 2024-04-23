using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Post
{
    public int PostId { get; set; }
    public int BlogId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public Blog Blog { get; set; } = null!;
    public List<Comment> Comments { get; set; } = [];
}
