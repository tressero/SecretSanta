using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
namespace SecretSanta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./myDb.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Create Table
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "CREATE TABLE users(name VARCHAR(50));";
                tableCmd.ExecuteNonQuery();

                //Insert Some Records
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO users VALUES('West Russell')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO users VALUES('Ann-Marie Thompson')";
                    insertCmd.ExecuteNonQuery();
                    insertCmd.CommandText = "INSERT INTO users VALUES('Jordan Wayburn')";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                //Read Records
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM users";
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader.GetString(0);
                        Console.WriteLine(result);
                    }
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}