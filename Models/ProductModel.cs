namespace CRUD2.Models
{
    // Class representing a product model in the application
    public class Product
    {
        // Property to hold the unique identifier of the product
        public int Id { get; set; } 

        // Property to hold the name of the product
        public string Name { get; set; } 

        // Property to hold a description of the product
        public string Description { get; set; } 

        // Property to hold the price of the product
        public decimal Price { get; set; }

        // Property to hold the name of the seller of the product
        public string Seller { get; set; } 
    }
}
