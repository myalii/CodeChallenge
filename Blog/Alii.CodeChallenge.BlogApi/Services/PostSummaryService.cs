using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace Alii.CodeChallenge.BlogApi.Services;

public class PostSummaryService(ILogger<PostSummaryService> logger, BlogContext blogContext)
{
    public async Task<List<PostSummaryDto>> GetPostsSummaryForUserAsync(int userId)
    {
        // TODO: Implement this method
        logger.LogError("GetPostsSummaryForUserAsync is not implemented");
        throw new NotImplementedException();
    }
}
