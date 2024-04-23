# Alii Code Challenge
Code challenge for new developers.

## Background Information
You are provided with a simplified data model representing a blogging system. The system includes four main entities: Blog, Post, Comment, and User.

## Challenge Task
Your task is to implement the GetPostsSummaryForUserAsync method that retrieves a summary of Post objects for a specific user. The summary should include the PostId, Title, and the number of comments for each post.

- The method should meet the following requirements:
- Only include posts that belong to a user specified by UserId.
- Only include posts that contain at least one comment.
- Perform validation checks invalid inputs or when the user does not exist.
- Return a list of PostSummaryDto objects.

## Evaluation Criteria
Candidates will be evaluated based on:
- Correct usage of Include(), navigation properties, and Where() clauses.
- Efficient use of IQueryable.
- Proper implementation of input validation and use of early exits.
- Code readability and adherence to C# coding conventions.
