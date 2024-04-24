
namespace Alii.CodeChallenge.BlogApi.Dto;

public class PostCreateDto
{
    public int BlogId { get; set; }
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
}
