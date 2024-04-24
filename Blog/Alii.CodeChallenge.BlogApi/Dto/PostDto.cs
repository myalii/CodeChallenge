namespace Alii.CodeChallenge.BlogApi.Dto;

public class PostDto
{
    public int PostId { get; set; }
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public List<CommentDto> Comments { get; set; } = [];
}
