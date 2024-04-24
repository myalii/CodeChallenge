# Alii Code Challenge
Welcome to the Alii Code Challenge! This challenge is designed for new developer candidates to demonstrate their coding skills, problem-solving abilities, and familiarity with ASP.NET Core and Entity Framework Core.

## Project Description
This coding challenge is centered around an established and functional .NET 8 minimal web API project. The platform in question is a blogging system, where the primary functionalities revolve around creating and managing blogs, posts, and user interactions. As a candidate, your role involves enhancing and adding new features to the existing codebase, not creating it from scratch.

The current infrastructure includes:
- **.NET 8 Minimal API**: The project is set up using the latest minimal API features introduced in .NET 8, offering a clean and lightweight way to define HTTP routes and services.
- **JWT Authentication**: The platform is secured with JWT (JSON Web Tokens) authentication, ensuring that endpoints are protected and that users are authenticated before they can interact with the system.
- **EF Core SQLite Database**: Entity Framework Core is utilized as the ORM (Object-Relational Mapper) for data access, with SQLite as the database.
- **XUnit Test Project**: Quality assurance is facilitated through an accompanying XUnit test project.

The entities you will encounter and possibly interact with in the project are:
- **Blog**: Represents a collection of posts, each blog is tied to a user who is the author.
- **Post**: Represents an individual article or entry within a blog, which can be written, edited, or deleted by the blog's author.
- **Comment**: Represents user engagement and discussions, allowing readers to leave feedback or thoughts on a post.
- **User**: Represents the author of a blog, who can create and manage their own posts and blogs.

The challenge is designed to be accessible, focusing on the addition of new features to an already operational platform. This is your chance to demonstrate your ability to work with modern .NET technologies and to contribute to an existing system with clean and maintainable code.

We are excited to see your solutions and wish you the best of luck!

## Challenge Tasks
### Task 1: GetPostsSummaryForUserAsync 
Implement the GetPostsSummaryForUserAsync method in the PostService class. This method retrieves a summary of Post objects for a specific user. The summary should include the PostId, Title, and the number of comments for each post.

The method must satisfy the following requirements:
- Only include posts that belong to the user specified by UserId.
- Only include posts that contain at least one comment.
- Perform necessary validation checks.
- Return a list of PostSummaryDto objects.

### Task 2: UpdatePostAsync
Implement the UpdatePostAsync method in the PostService class. This method allows an authenticated user to update an existing post.

The method must satisfy the following requirements:
- Perform necessary validation checks.
- Use a reputable library like Microsoft.Security.Application to sanitize fields to prevent XSS attacks.
- Update only the Title and Content of the post.
- Return the updated PostDto object.

## Evaluation Criteria
 
Candidates will be evaluated based on the following criteria:
- Correct and efficient usage of IQueryable, Include(), navigation properties, and Where() clauses.
- Proper input validation and use of early exits.
- Code readability and adherence to C# coding conventions.
- Appropriate authorization to ensure that users can only access and modify their own data.

Candidates are encouraged to demonstrate best practices in their code and to provide clear and maintainable solutions.

Good luck, and happy coding!
