namespace BlueprintBackend.Models;

public record Project
(
    string Id,
    string ProjectName,
    DateTime LastModified,
    string ProjectFile
);
