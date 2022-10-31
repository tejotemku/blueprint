﻿using Npgsql;
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
        con.Open();
        cmd = new NpgsqlCommand();
        cmd.Connection = con;
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS usersData(username VARCHAR(255) PRIMARY KEY, email VARCHAR(255) UNIQUE, passwordHash TEXT, passwordSalt TEXT)");
        ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS projects(id SERIAL PRIMARY KEY, name VARCHAR(255), description TEXT, file JSONB, owner VARCHAR(255) references usersData(username))");
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

    public int CreateProject(string name, string file, string description, string owner)
    {
        cmd.CommandText = $"INSERT INTO projects(name, file, description, owner) VALUES('{name}','{file}', '{description}', '{owner}') RETURNING id";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return rdr.GetInt32(0);
    }

    public void GetUserPaswordHashAndSalt(string username, out string passwordHash, out string passwordSalt) 
    {
        cmd.CommandText = $"SELECT passwordHash, passwordSalt FROM usersData WHERE username='{username}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        passwordHash = rdr.GetString(0);
        passwordSalt = rdr.GetString(1);
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
        cmd.CommandText = $"SELECT id, name, description FROM projects WHERE owner='{username}'";
        using var rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            data.Add(new ProjectInfoDto(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
        }
        return data;
    }

    public string GetProjectFile(int id)
    {
        cmd.CommandText = $"SELECT file FROM projects WHERE id='{id}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0));
    }

    public void UpdateProjectFile(int id, string file)
    {
        ExecutePostgresNonQueryCommand($"UPDATE projects SET file='{file}' WHERE id='{id}'");
    }

    public string GetProjectOwner(int id)
    {
        cmd.CommandText = $"SELECT owner FROM projects WHERE id='{id}'";
        using var rdr = cmd.ExecuteReader();
        rdr.Read();
        return (rdr.GetString(0));

    }

    public void DeleteProject(int id)
    {
        ExecutePostgresNonQueryCommand($"DELETE FROM projects WHERE id='{id}'");
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