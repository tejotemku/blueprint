
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using BlueprintBackend.Interfaces;

namespace BlueprintBackend
{
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
/*            ExecutePostgresNonQueryCommand("DROP TABLE IF EXISTS usersData");
            ExecutePostgresNonQueryCommand("DROP TABLE IF EXISTS projects");*/
            ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS usersData(id SERIAL PRIMARY KEY, username VARCHAR(255) UNIQUE, email VARCHAR(255) UNIQUE, passwordHash TEXT, passwordSalt TEXT)");
            ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS projects(id SERIAL PRIMARY KEY, projectName VARCHAR(255), projectFile JSONB, lastModified DATE, userID SERIAL references usersData(id))");
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

        public void InsertProject(string projectName, string projectFile, string userID)
        {
            ExecutePostgresNonQueryCommand($"INSERT INTO projects(projectName, projectFile, lastModified, userID) VALUES('{projectName}','{projectFile}', '{DateTime.Now.ToString()}'), '{userID}')");
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

        public List<(string, DateTime)> GetUsersProjectsData(string userId)
        {
            List<(string, DateTime)> data = new(); 
            cmd.CommandText = $"SELECT projectName, lastModified FROM projects WHERE id='{userId}'";
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                data.Add((rdr.GetString(0), rdr.GetDateTime(1)));
            }
            return data;
        }



/*
        public string GetProjectFile(string projectID)
        {
            cmd.CommandText = $"SELECT projectFile FROM projects WHERE id='{projectID}'";
            using var rdr = cmd.ExecuteReader();

        }
*/
    }
}
