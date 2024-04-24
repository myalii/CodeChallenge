namespace Alii.CodeChallenge.BlogApi.Dto;

public class CommentDto
{
    public int CommentId { get; set; }
    public required string Author { get; set; }
    public string Content { get; set; } = string.Empty;
}
