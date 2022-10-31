using BlueprintBackend.Models;

namespace BlueprintBackend.Interfaces;

public interface IDataBase
{
    public void InsertUser(string username, string email, string passwordHash, string passwordSalt);

    public int CreateProject(string name, string file, string description, string owner);

    public void GetUserPaswordHashAndSalt(string username, out string passwordHash, out string passwordSalt);

    public string GetUserEmail(string username);

    public List<ProjectInfoDto> GetUsersProjectsData(string username);

    public string GetProjectFile(int id);

    public void UpdateProjectFile(int id, string file);

    public string GetProjectOwner(int id);

    public void DeleteProject(int id);

    public bool CheckClaims(string username, string email);
}
