using MySql.Data.MySqlClient;

namespace CRUD2.Database
{
    public class ProductDatabase
    {   
        //change this to your connection string also change the value for the myConnectionString in program.cs, you will find it in "appsettings.Development.json"
        private const string ConnectionString = "Server=127.0.0.1;Database=CRUDAppDB;User=root;Password=<your-password>;";

        // Creates and returns a MySQL connection
        public MySqlConnection ConnectToDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Sets up the database schema if not already present
        public void SetUpDatabase()
        {
            using (var connection = ConnectToDatabase())
            {
                connection.Open();

                // Create database if it doesn't exist
                var createDatabaseCommand = new MySqlCommand("CREATE DATABASE IF NOT EXISTS CRUDAppDB;", connection);
                createDatabaseCommand.ExecuteNonQuery();

                // Select the database
                var useDatabaseCommand = new MySqlCommand("USE CRUDAppDB;", connection);
                useDatabaseCommand.ExecuteNonQuery();

                // Create Products table
                var createTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Products (
                        Product_id INT AUTO_INCREMENT PRIMARY KEY,
                        Product_Name VARCHAR(255) NOT NULL,
                        Product_Description TEXT,
                        Price DECIMAL(10,2) NOT NULL,
                        Seller VARCHAR(255) NOT NULL
                    );", connection);
                createTableCommand.ExecuteNonQuery();

                /* // Alter the Products table (example of adding a new column)
                var alterTableCommand = new MySqlCommand(@"
                    ALTER TABLE Products 
                    ADD COLUMN IF NOT EXISTS StockQuantity INT DEFAULT 0;", connection);
                alterTableCommand.ExecuteNonQuery();*/
            }
        }
    }
}
