namespace BlueprintBackend.Models;

public record CreateProjectDto
(
    string name,
    string owner,
    string description,
    string file
);
