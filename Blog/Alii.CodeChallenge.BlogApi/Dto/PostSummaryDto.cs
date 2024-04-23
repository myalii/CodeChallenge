namespace Alii.CodeChallenge.BlogApi.Dto;

public class PostSummaryDto
{
    public int PostId { get; set; }
    public required string Title { get; set; }
    public int CommentCount { get; set; }
}
