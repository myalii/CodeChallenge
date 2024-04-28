using Alii.CodeChallenge.BlogApi.Data.Models;

namespace Alii.CodeChallenge.BlogApi.Dto;

public class PostDto
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<CommentDto> Comments { get; set; } = [];

    public PostDto() { }

    public PostDto(Post post)
    {
        PostId = post.PostId;
        Title = post.Title;
        Content = post.Content;
        Comments = post.Comments.Select(c => new CommentDto
        {
            CommentId = c.CommentId,
            Author = c.Author,
            Content = c.Content
        }).ToList();
    }
}
