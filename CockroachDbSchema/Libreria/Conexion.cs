using System;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace CockroachDbSchema.Libreria
{
    public class Conexion
    {
        public string Id { set; get; } = string.Empty;
        public string Host { set; get; } = string.Empty;
        public int Port { set; get; } = 0;
        public bool SslMode { set; get; } = false;
        public string Username { set; get; } = string.Empty;
        public string Password { set; get; } = string.Empty;
        public string Database { set; get; } = string.Empty;

        public async Task<NpgsqlConnection> Devolver(Conexion conexion)
        {
            return await Task.Run(() =>
            {
                NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
                npgsqlConnectionStringBuilder.Host = conexion.Host;
                npgsqlConnectionStringBuilder.Port = conexion.Port;
                npgsqlConnectionStringBuilder.SslMode = conexion.SslMode ? Npgsql.SslMode.Require : Npgsql.SslMode.Disable;
                npgsqlConnectionStringBuilder.Username = conexion.Username;
                npgsqlConnectionStringBuilder.Password = conexion.Password;
                npgsqlConnectionStringBuilder.Database = conexion.Database;
                npgsqlConnectionStringBuilder.TrustServerCertificate = true;
                NpgsqlConnection mySqlConnection = new NpgsqlConnection();
                mySqlConnection.ConnectionString = npgsqlConnectionStringBuilder.ToString();
                mySqlConnection.Open();
                return mySqlConnection;
            });
        }

        public async Task Test(Conexion conexion)
        {
            await Task.Run(async () =>
            {
                using (var db = await Devolver(conexion))
                {
                    NpgsqlCommand sqlCommand = new NpgsqlCommand();
                    sqlCommand.Connection = db;
                    sqlCommand.CommandText = $"SELECT 1 AS \"xD\";";
                    sqlCommand.CommandType = CommandType.Text;
                    using (NpgsqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Convert.ToString(sqlDataReader["xD"]);
                        }
                    }
                }
            });
        }
    }
}
