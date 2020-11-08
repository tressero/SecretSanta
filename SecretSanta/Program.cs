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
using Microsoft.EntityFrameworkCore;
using SecretSanta.Models;

namespace SecretSanta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new SecretSantaContext())
            {
                // Create Users
                Console.WriteLine("Inserting new users");
                db.Add(new User
                {
                    FirstName = "Marc",
                    LastName = "Ochsner",
                    Email = "testemail@gmail.com",
                    RecipientId = 11
                });
                db.Add(new User
                {
                    FirstName = "Ann-Marie",
                    LastName = "Thompson",
                    Email = "testemail@gmail.com",
                    RecipientId = 12
                });
                db.Add(new User
                {
                    FirstName = "Jordan",
                    LastName = "Wayburn",
                    Email = "testemail@gmail.com",
                    RecipientId = 13
                });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying Users");
                var user = db.Users
                    .OrderBy(b => b.UserId)
                    .First();
                Console.WriteLine(user.UserId);

                // Update
                Console.WriteLine("Updating by adding a Present");
                user.Presents.Add (new Present
                {
                    Date = DateTime.Today,
                    Url = "https://www.amazon.com/8-0-NET-Core-3-0-Cross-Platform/dp/1788478126/ref=sr_1_1_sspa?dchild=1&keywords=entity+framework&qid=1604820888&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUEzQTI3MUQ3VkFXOTU1JmVuY3J5cHRlZElkPUEwODA0Njk3Mzk5T1dHWjBDMUdONiZlbmNyeXB0ZWRBZElkPUEwMTk5Mzc0M0QxOTROUDFLMEtEWiZ3aWRnZXROYW1lPXNwX2F0ZiZhY3Rpb249Y2xpY2tSZWRpcmVjdCZkb05vdExvZ0NsaWNrPXRydWU=",
                    Price = "39.99"
                });

                db.SaveChanges();

                // Delete User
                // Console.WriteLine("Delete the first user");
                // db.Remove(user);
                // db.SaveChanges();
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}