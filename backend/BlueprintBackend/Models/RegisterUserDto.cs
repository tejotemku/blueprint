namespace BlueprintBackend.Models;

public record RegisterUserDto
(
    string username,
    string email,
    string password
);
