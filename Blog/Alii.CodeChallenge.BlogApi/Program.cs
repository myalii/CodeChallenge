using System.Security.Claims;
using System.Text;
using Alii.CodeChallenge.BlogApi.Data;
using Alii.CodeChallenge.BlogApi.Data.Models;
using Alii.CodeChallenge.BlogApi.Dto;
using Alii.CodeChallenge.BlogApi.Endpoints;
using Alii.CodeChallenge.BlogApi.Extensions;
using Alii.CodeChallenge.BlogApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Alii.CodeChallenge.BlogApi", Version = "v1" });

    // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)  
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization", // Name of the header  
        In = ParameterLocation.Header, // Location of the header  
        Type = SecuritySchemeType.Http, // Type of the security scheme  
        Scheme = "bearer", // The scheme used (HTTP bearer authentication)  
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlogDatabase")));
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Key"] ?? throw new InvalidOperationException();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

app.SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/login", async (UserService userService, TokenService tokenService, UserLoginDto userLoginDto) =>
{
    var user = await userService.Authenticate(userLoginDto.Username, userLoginDto.Password);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    var token = tokenService.GenerateToken(user);
    return Results.Ok(new { Token = token });
});
app.AddBlogEndpoints();
app.AddPostEndpoints();

app.Run();
