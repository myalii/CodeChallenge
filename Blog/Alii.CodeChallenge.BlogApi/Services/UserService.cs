using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Alii.CodeChallenge.BlogApi.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly BlogContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(ILogger<UserService> logger, BlogContext context, PasswordHasher<User> passwordHasher)
    {
        _logger = logger;
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == username);

        if (user == null)
        {
            return null; // User not found
        }

        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
        {
            return null; // Authentication failed
        }

        return user; // Authentication success
    }
}
