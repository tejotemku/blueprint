using Npgsql;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Models;

namespace BlueprintBackend.Services;
public class PostgresDBmanager: IDataBase
{
    private readonly IConfiguration _config;
    private NpgsqlConnection con;
    NpgsqlCommand cmd;

public PostgresDBmanager(IConfiguration config)
    {
        _config = config;
        var cs = _config.GetValue<string>("DATABASE_URL");
        con = new NpgsqlConnection(cs);            
        con.Open();
        cmd = new NpgsqlCommand();
        cmd.Connection = con;
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS usersData(username VARCHAR(255) PRIMARY KEY, email VARCHAR(255) UNIQUE, passwordHash TEXT, passwordSalt TEXT)");
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS projects(id SERIAL PRIMARY KEY, projectName VARCHAR(255), projectFile JSONB, username VARCHAR(255) references usersData(username))");
    }

    private void ExecutePostgresNonQueryCommand(string command)
    {
        cmd.CommandText = command;
        Console.WriteLine(cmd.ExecuteNonQuery().ToString());
    }

    public void InsertUser(string username, string email, string passwordHash, string passwordSalt)
    {
        ExecutePostgresNonQueryCommand($"INSERT INTO usersData(username, email, passwordHash, passwordSalt) VALUES('{username}','{email}', '{passwordHash}', '{passwordSalt}')");
    }

    public int CreateProject(string projectName, string projectFile, string username)
    {
        cmd.CommandText = $"INSERT INTO projects(projectName, projectFile, username) VALUES('{projectName}','{projectFile}', '{username}') RETURNING id";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return rdr.GetInt32(0);
    }

    public (string, string) GetUserPaswordHashAndSalt(string username) 
    {
        cmd.CommandText = $"SELECT passwordHash, passwordSalt FROM usersData WHERE username='{username}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0), rdr.GetString(1));
    }

    public string GetUserEmail(string username)
    {
        cmd.CommandText = $"SELECT email FROM usersData WHERE username='{username}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0));
    }

    public List<ProjectInfoDto> GetUsersProjectsData(string username)
    {
        List<ProjectInfoDto> data = new(); 
        cmd.CommandText = $"SELECT id, projectName FROM projects WHERE username='{username}'";
        using var rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            data.Add(new ProjectInfoDto(rdr.GetInt32(0), rdr.GetString(1)));
        }
        return data;
    }

    public string GetProjectFile(int projectId)
    {
        cmd.CommandText = $"SELECT projectFile FROM projects WHERE id='{projectId}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0));
    }

    public void UpdateProjectFile(int projectId, string projectFile)
    {
        ExecutePostgresNonQueryCommand($"UPDATE projects SET projectFile='{projectFile}' WHERE id='{projectId}'");
    }

    public string GetProjectOwner(int projectId)
    {
        cmd.CommandText = $"SELECT username FROM projects WHERE id='{projectId}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0));

    }

    public void DeleteProject(int projectId)
    {
        ExecutePostgresNonQueryCommand($"DELETE FROM projects WHERE id='{projectId}'");
    }
    public bool CheckClaims(string username, string email)
    {
        cmd.CommandText = $"SELECT username, email FROM usersData WHERE username='{username}' AND email='{email}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        var db_username = rdr.GetString(0);
        var db_email = rdr.GetString(0);
        return db_username == username && db_email == email;
    }
}
