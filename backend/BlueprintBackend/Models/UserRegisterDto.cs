namespace BlueprintBackend.Models;

public record UserRegisterDto
(
     string Username, 
     string Email,
     string PasswordHash,
     string PasswordSalt
);
