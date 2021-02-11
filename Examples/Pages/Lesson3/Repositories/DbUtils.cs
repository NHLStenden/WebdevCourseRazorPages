using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace Examples.Pages.Lesson3.Repositories
{
    public static class DbUtils
    {
        public static IDbConnection GetDbConnection()
        {
            string connectionString =
                Startup.StaticConfiguration.GetConnectionString("WebdevCourseRazorPages.Example.MySQL");

            return new MySqlConnection(connectionString);

            // string connStr =
            //     Startup.StaticConfiguration.GetConnectionString("WebdevCourseRazorPages.Example.PostgreSQL");
            // return new NpgsqlConnection(connStr);
            //"Server=127.0.0.1;Port=5432;Database=Examples;User Id=postgres;Password=root;"
        }
    }
}
