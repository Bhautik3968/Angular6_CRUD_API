using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestAPI.DataContext
{
    public class DatabaseConnection
    {
        public IDbConnection ClientDBConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString);
            }
        }
    }
}