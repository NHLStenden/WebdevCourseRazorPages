using System.Data;
using MySql.Data.MySqlClient;

namespace NHLStendenCafe.Repositories
{
    public class DbUtils : IDbUtils
    {
        public static string ConnectionSting { get; set; }
        
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(ConnectionSting);
        }
    }

    public interface IDbUtils
    {
        IDbConnection GetDbConnection();
    }
}
