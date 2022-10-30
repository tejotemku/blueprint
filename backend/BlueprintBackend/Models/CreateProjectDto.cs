namespace BlueprintBackend.Models
{
    public record CreateProjectDto
    (
        string projectName, 
        string projectFile, 
        string username
    );
}
