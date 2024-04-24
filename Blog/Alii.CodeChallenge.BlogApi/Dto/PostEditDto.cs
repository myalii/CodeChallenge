namespace Alii.CodeChallenge.BlogApi.Dto;

public class PostEditDto
{
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
}
