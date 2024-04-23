using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alii.CodeChallenge.BlogApi.Data.Models;

public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public List<Blog> Blogs { get; set; } = [];
}
