using System.Data;
using MySqlConnector;

namespace Exercises.Tests
{
    public static class DbUtils
    {
        public static IDbConnection GetDbConnection()
        {
            return new MySqlConnection("Server=127.0.0.1;Port=3306;Database=Tests;Uid=root;Pwd=Test@1234!;");
        }
    }
}
