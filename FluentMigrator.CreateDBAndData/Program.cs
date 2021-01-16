using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text;

namespace FluentMigrator.CreateDBAndData
{
    class Program
    {
        const string dbName = "siteDB";
        static void Main(string[] args)
        {
            Console.Write("Write your connect string without database: ");
            string connString = Console.ReadLine();// @"Server=localhost\SQLEXPRESS;Trusted_Connection=True;";
            connString = ValidateConnString(connString);
            Database.EnsureDatabase(connString, dbName);
            connString = connString + "Database=" + dbName;
            var serviceProvider = CreateServices(connString);
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        private static string ValidateConnString(string str)
        {
            var arr = str.Split(';');
            StringBuilder sb = new StringBuilder();
            foreach(var s in arr)
            {
                if(!s.StartsWith("Database") || s.StartsWith("database"))
                {
                    sb.Append(s + ";");
                }
            }
            return sb.ToString();
        }

        private static IServiceProvider CreateServices(string connString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
