using CRUD2.Database;
using CRUD2.Models;
using MySql.Data.MySqlClient;

namespace CRUD2.Services
{
    public class ProductServices
    {
        private readonly ProductDatabase _productDatabase;

        // Constructor to initialize the ProductServices class with a ProductDatabase instance
        public ProductServices(ProductDatabase productDb)
        {
            _productDatabase = productDb;
        }

        // Method to add a new product to the database
        public void AddProduct(Product product)
        {
            // Establish a connection to the database
            using (var connection = _productDatabase.ConnectToDatabase())
            {
                connection.Open();
                // Define the SQL query to insert a new product
                var query = "INSERT INTO Products (Product_Name, Product_Description, Price, Seller) VALUES (@Name, @Description, @Price, @Seller);";

                // Prepare and execute the query with parameters
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Seller", product.Seller);

                    command.ExecuteNonQuery(); // Execute the query
                }
            }
        }

        // Method to delete a product from the database by its ID
        public void DeleteProduct(int id)
        {
            // Establish a connection to the database
            using (var connection = _productDatabase.ConnectToDatabase())
            {
                connection.Open();
                // Define the SQL query to delete a product by its ID
                var query = "DELETE FROM Products WHERE Product_id = @Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id); // Add the product ID parameter
                    command.ExecuteNonQuery(); // Execute the query
                }
            }
        }

        // Method to retrieve all products from the database
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>(); // List to store all products
            // Establish a connection to the database
            using (var connection = _productDatabase.ConnectToDatabase())
            {
                connection.Open();
                // Define the SQL query to select all products
                var query = "SELECT * FROM Products;";
                using (var command = new MySqlCommand(query, connection))
                {
                    // Execute the query and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        // Loop through each record and add it to the product list
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32("Product_id"),
                                Name = reader.GetString("Product_Name"),
                                Description = reader.GetString("Product_Description"),
                                Price = reader.GetDecimal("Price"),
                                Seller = reader.GetString("Seller")
                            });
                        }
                    }
                }
            }
            return products; // Return the list of all products
        }

        // Method to update an existing product in the database
        public void UpdateProduct(Product product)
        {
            // Establish a connection to the database
            using (var connection = _productDatabase.ConnectToDatabase())
            {
                connection.Open();
                // Define the SQL query to update a product's details
                var query = "UPDATE Products SET Product_Name = @Name, Product_Description = @Description, Price = @Price, Seller = @Seller WHERE Product_id = @Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Seller", product.Seller);
                    command.Parameters.AddWithValue("@Id", product.Id); // Add the product ID parameter

                    command.ExecuteNonQuery(); // Execute the query to update the product
                }
            }
        }

        // Method to retrieve a product by its name from the database
        public Product GetProductByName(string name)
        {
            // Establish a connection to the database
            using (var connection = _productDatabase.ConnectToDatabase())
            {
                connection.Open();
                // Define the SQL query to select a product by name
                var query = "SELECT * FROM Products WHERE Product_Name = @Name LIMIT 1;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name); // Add the product name parameter
                    using (var reader = command.ExecuteReader())
                    {
                        // If a product is found, return it
                        if (reader.Read())
                        {
                            return new Product
                            {
                                Id = reader.GetInt32("Product_id"),
                                Name = reader.GetString("Product_Name"),
                                Description = reader.GetString("Product_Description"),
                                Price = reader.GetDecimal("Price"),
                                Seller = reader.GetString("Seller")
                            };
                        }
                    }
                }
            }
            return null; // Return null if no product is found by the given name
        }
    }
}
