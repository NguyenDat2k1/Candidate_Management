using MySqlConnector;
namespace Interview.Models
{
    public class mySQLSqlHelper
    {
        //this field gets initialized at Startup.cs
       // public static string conStr;
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;user id=root;password=20150601;port=3306;database=interview;");
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}