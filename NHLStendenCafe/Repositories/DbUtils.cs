using System.Data;
using MySql.Data.MySqlClient;

namespace NHLStendenCafe.Repositories
{
    public static class DbUtils
    {
        public static string ConnectionSting { get; set; }
        
        public static IDbConnection GetDbConnection()
        {
            return new MySqlConnection(ConnectionSting);
        }
    }
}
