using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Exercises.Pages.Lesson3
{
    public class DbUtils : IDbUtils
    {
        public IDbConnection GetDbConnection()
        {
            string connectionString =
                Startup.StaticConfiguration.GetConnectionString("WebdevCourseRazorPages.Exercises.MySQL");

            return new MySqlConnection(connectionString);
        }
    }

    public interface IDbUtils
    {
        IDbConnection GetDbConnection();
    }
}
