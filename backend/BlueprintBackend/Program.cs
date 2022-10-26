using Npgsql;

var cs = "Server=127.0.0.1;Port=5432;Username=postgres;Password=password;Database=postgres";

using var con = new NpgsqlConnection(cs);
con.Open();

var sql = "SELECT version()";

using var cmd = new NpgsqlCommand(sql, con);

var version = cmd.ExecuteScalar().ToString();
Console.WriteLine($"PostgreSQL version: {version}");
