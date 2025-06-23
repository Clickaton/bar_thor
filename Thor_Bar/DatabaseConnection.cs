using System.Data;
using System.Data.SQLite;

namespace Thor_Bar
{
    public class DatabaseConnection
    {
        private string connectionString = "Data Source=thor_bar.sqlite;Version=3;";
        private SQLiteConnection connection;

        public DatabaseConnection()
        {
            connection = new SQLiteConnection(connectionString);
        }

        public SQLiteConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
