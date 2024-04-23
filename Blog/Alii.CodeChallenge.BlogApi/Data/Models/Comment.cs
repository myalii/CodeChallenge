using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class Comment
{
    public int CommentId { get; set; }
    public int PostId { get; set; }
    public required string Author { get; set; }
    public required string Content { get; set; }
    public Post Post { get; set; } = null!;
}
