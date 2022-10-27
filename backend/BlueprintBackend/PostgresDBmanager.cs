
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace BlueprintBackend
{
    public class PostgresDBmanager
    {
        NpgsqlConnection con;

        public PostgresDBmanager()
        {
            var cs = "Server=127.0.0.1;Port=5432;Username=postgres;Password=password;Database=postgres";
            con = new NpgsqlConnection(cs);            
            con.Open();
            ExecutePostgresNonQueryCommand("DROP TABLE IF EXISTS usersData");
            ExecutePostgresNonQueryCommand("DROP TABLE IF EXISTS projects");
            ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS usersData(id SERIAL PRIMARY KEY, username VARCHAR(255), email VARCHAR(255), passwordHash VARCHAR(255))");
            ExecutePostgresNonQueryCommand("CREATE TABLE IF NOT EXISTS projects(id SERIAL PRIMARY KEY, projectName VARCHAR(255), projectFile JSONB, lastModified DATE, userID SERIAL references userData(id))");




/* 
            ExecutePostgresNonQueryCommand("DROP TABLE IF EXISTS cars");

            ExecutePostgresNonQueryCommand("CREATE TABLE cars(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)");

            ExecutePostgresNonQueryCommand("INSERT INTO cars(name, price) VALUES('Audi',52642)");

            ExecutePostgresReaderCommand("Select * from cars");
*/
        }


        void ExecutePostgresNonQueryCommand(string command)
        {
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = command;
            //Console.WriteLine(cmd.ExecuteNonQuery().ToString());
        }

        void InsertUser(string username, string email, string passwordHash)
        {
            ExecutePostgresNonQueryCommand($"INSERT INTO usersData(username, email, passwordHash) VALUES('${username}','${email}', '${passwordHash}')");
        }

        void InsertProject(string projectName, string projectFile, string userID)
        {
            ExecutePostgresNonQueryCommand($"INSERT INTO projects(projectName, projectFile, lastModified, userID) VALUES('${projectName}','${projectFile}', '${DateTime.Now.ToString()}'), '${userID}')");
        }

        List<(string, DateTime)> GetUsersProjectsData(string userId)
        {
            List<(string, DateTime)> data = new(); 
            string command = $"SELECT projectName, lastModified FROM projects WHERE id='${userId}'";
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = command;
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                data.Add((rdr.GetString(0), rdr.GetDateTime(1)));
            }
            return data;
        }



/*
        string GetProjectFile(string projectID)
        {
            string command = $"SELECT projectFile FROM projects WHERE id='${projectID}'";
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = command;
            using var rdr = cmd.ExecuteReader();

        }
*/
        void ExecutePostgresReaderCommand(string command)
        {
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            cmd.CommandText = command;
            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("{0},{1}, {2}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));
            }
        }
    }
}
