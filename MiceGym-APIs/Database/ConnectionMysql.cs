using MySql.Data.MySqlClient;
using System.Data;

namespace MiceGym_APIs.Database
{
    public class ConnectionMysql
    {

        private static readonly string host = "localhost";

        private static readonly string port = "3360";

        private static readonly string user = "root";

        private static readonly string password = "root";

        private static readonly string dbname = "mice_gym_bd";

        private static MySqlConnection connection;

        private static MySqlCommand command;

        public ConnectionMysql()
        {
            try
            {
                connection = new MySqlConnection($"server={host};database={dbname};port={port};user={user};password={password}");
                connection.Open();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public MySqlCommand Query()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                return command;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Open()
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
