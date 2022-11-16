using Npgsql;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Models;

namespace BlueprintBackend.Services;
public class PostgresDBService: IDataBase
{
    private readonly IConfiguration _config;
    private NpgsqlConnection con;
    NpgsqlCommand cmd;

public PostgresDBService(IConfiguration config)
    {
        _config = config;
        var cs = _config.GetValue<string>("DATABASE_URL");
        con = new NpgsqlConnection(cs);            
        cmd = new NpgsqlCommand();
        cmd.Connection = con;
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS usersData(username VARCHAR(255) PRIMARY KEY, email VARCHAR(255) UNIQUE, passwordHash TEXT, passwordSalt TEXT)");
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS projects(id SERIAL PRIMARY KEY, name VARCHAR(255), description TEXT, file JSONB, owner VARCHAR(255) references usersData(username))");
    }

    private void ExecutePostgresNonQueryCommand(string command)
    {
        con.Open();
        cmd.CommandText = command;
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void InsertUser(string username, string email, string passwordHash, string passwordSalt)
    {
        string commandText = $"INSERT INTO usersData(username, email, passwordHash, passwordSalt) VALUES(@username, @email, @passwordHash, @passwordSalt)";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {

            command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("passwordHash", passwordHash);
            command.Parameters.AddWithValue("passwordSalt", passwordSalt);
            command.ExecuteNonQuery();
        }
        con.Close();
    }

    public int CreateProject(string name, string file, string description, string owner)
    {
        con.Open();
        string commandText = $"INSERT INTO projects(name, file, description, owner) VALUES(@name, '{file}', @description, @owner) RETURNING id";
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("description", description);
            command.Parameters.AddWithValue("owner", owner);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            int result = rdr.GetInt32(0);
            return result;
        }
        con.Close();
    }

    public void GetUserPaswordHashAndSalt(string username, out string passwordHash, out string passwordSalt)
    {
        con.Open();
        string commandText = $"SELECT passwordHash, passwordSalt FROM usersData WHERE username=@username";
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("username", username);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            passwordHash = rdr.GetString(0);
            passwordSalt = rdr.GetString(1);
        }
        con.Close();
    }

    public string GetUserEmail(string username)
    {
        string commandText = $"SELECT email FROM usersData WHERE username=@username";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        { 
            command.Parameters.AddWithValue("username", username);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            string result = rdr.GetString(0);
            con.Close();
            return result;
        }
        con.Close();
    }

    public List<ProjectInfoDto> GetUsersProjectsData(string username)
    {
        List<ProjectInfoDto> data = new();
        string commandText = $"SELECT id, name, description FROM projects WHERE owner=@username";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("username", username);
            using var rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                data.Add(new ProjectInfoDto(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
            }
            con.Close();
            return data;
        }
        con.Close();
    }

    public string GetProjectFile(int id)
    {
        string commandText = $"SELECT file FROM projects WHERE id=@id";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("id", id);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            string result = rdr.GetString(0);
            con.Close();
            return result;
        }
        con.Close();

    }

    public void UpdateProjectFile(int id, string file)
    {
        string commandText = $"UPDATE projects SET file='{file}' WHERE id=@id";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }
        con.Close();

    }

    public string GetProjectOwner(int id)
    {
        string commandText = $"SELECT owner FROM projects WHERE id=@id";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("id", id);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            string result = rdr.GetString(0);
            con.Close();
            return result;
        }
        con.Close();
    }

    public void DeleteProject(int id)
    {
        string commandText = $"DELETE FROM projects WHERE id=@id";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }
        con.Close();
    }

    public bool CheckClaims(string username, string email)
    {
        string commandText = $"SELECT username, email FROM usersData WHERE username=@username AND email=@email";
        con.Open();
        using (var command = new NpgsqlCommand(commandText, con))
        {
            command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("email", email);
            using var rdr = command.ExecuteReader();
            rdr.Read();
            var db_username = rdr.GetString(0);
            var db_email = rdr.GetString(1);
            con.Close();
            return db_username == username && db_email == email;
        }
        con.Close();

    }
}
