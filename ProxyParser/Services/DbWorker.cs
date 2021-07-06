using Microsoft.Data.Sqlite;
using ProxyParser.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProxyParser.Services
{
    public class DbWorker
    {
        readonly string source;
        string tableName;
        public DbWorker()
        {
            source = "Data Source=proxy_data.db";
            tableName = "ProxyServers";
        }

        public async Task<int> InsertProxyList(List<ProxyInDb> proxies)
        {
            using (var connection = new SqliteConnection(source))
            {
                string insertValues = GetStrValues(proxies);
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO {tableName} (Type, IP, Port, Country, Anonymity) VALUES {insertValues}";
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async void DeleteById (int id)
        {
            string sqlExpression = $"DELETE  FROM {tableName} WHERE id ={id}";
            using (var connection = new SqliteConnection(source))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                await command.ExecuteNonQueryAsync();
            }
        }

        public List<ProxyInDb> GetProxies()
        {
            List<ProxyInDb> result = new List<ProxyInDb>();

            string sqlExpression = $"SELECT * FROM {tableName}";
            using (var connection = new SqliteConnection(source))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ProxyInDb tmpProxy = new ProxyInDb
                            {
                                id = reader.GetInt32(0),
                                ProxyType = reader.GetString(1),
                                IP = reader.GetString(2),
                                Port = reader.GetInt32(3),
                                Country = reader.GetString(4),
                                Anonymity = reader.GetString(5)
                            };
                            result.Add(tmpProxy);
                        }
                    }
                }
            }
            return result;
        }

        private string GetStrValues(List<ProxyInDb> proxies)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var proxy in proxies)
            {
                sb.Append($"('{proxy.ProxyType}', '{proxy.IP}', {proxy.Port}, '{proxy.Country}', '{proxy.Anonymity}'),");
            }
            return sb.ToString().Trim(',');
        }

        public void CreateTable()
        {
            using (var connection = new SqliteConnection(source))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"CREATE TABLE {tableName} (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "Type TEXT NOT NULL, " +
                    "IP TEXT NOT NULL, " +
                    "Port INTEGER NOT NULL, " +
                    "Country TEXT, " +
                    "Anonymity TEXT)";
                command.ExecuteNonQuery();
            }
        }
    }
}
