using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseConnLibrary
{
    public class DatabaseConnection
    {
        public string tableName { get; set; }
        public string password { get; set; }
        public string databaseName { get; set; }
        public string username { get; set; }

        private DataTable table;
        private MySqlDataAdapter adapter;

        MySqlConnection connection = null;

        public DatabaseConnection(string databaseName, string username, string password)
        {
            table = new DataTable();
            adapter = new MySqlDataAdapter();

            this.username = username;
            this.databaseName = databaseName;
            this.password = password;
        }

        public void OpenConnection()
        {
            connection = new MySqlConnection($"Server=localhost; Database={databaseName}; User ID={username}; Password={password}");
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public DataTable GetTableData(string tableName)
        {
            OpenConnection();
            if (connection == null)
            {
                return null;
            }


            DataTable table = new DataTable();
            string SqlCommand = $"SELECT * FROM {databaseName}.{tableName};";
            Console.WriteLine(SqlCommand);
            MySqlCommand command = new MySqlCommand(SqlCommand, GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            CloseConnection();
            return table;
        }
    }
}
