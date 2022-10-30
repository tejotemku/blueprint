using BlueprintBackend.Models;

namespace BlueprintBackend.Interfaces;

public interface IDataBase
{
    public void InsertUser(string username, string email, string passwordHash, string passwordSalt);

    public int CreateProject(string projectName, string projectFile, string username);

    public (string, string) GetUserPaswordHashAndSalt(string username);

    public string GetUserEmail(string username);

    public List<ProjectInfoDto> GetUsersProjectsData(string username);

    public string GetProjectFile(int projectID);

    public void UpdateProjectFile(int projectId, string projectFile);

    public void DeleteProject(int projectId);

    public bool CheckClaims(string username, string email);

    public string GetProjectOwner(int projectId);
}
