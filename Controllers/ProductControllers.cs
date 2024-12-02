using Microsoft.AspNetCore.Mvc;
using CRUD2.Models;
using CRUD2.Services;

namespace CRUD2.Controllers
{
    // Controller to handle product-related HTTP requests
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductServices _productServices;

        // Constructor to inject ProductServices into the controller
        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        // POST method to add a new product
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product productToBeAdded)
        {
            try
            {
                // Call the service to add the product
                _productServices.AddProduct(productToBeAdded);
                // Return success response if product is added successfully
                return Ok(new { message = "Product added successfully" });
            }
            catch (Exception ex)
            {
                // Return error response if there's an exception
                return BadRequest(new { message = $"Error adding product: {ex.Message}" });
            }
        }

        
        // DELETE method to remove a product by its ID
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            // Call the service to delete the product
            _productServices.DeleteProduct(id);
            // Return success response after deletion
            return Ok(new { message = "Product deleted successfully" });
        }

        // GET method to retrieve all products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            // Call the service to get all products
            var products = _productServices.GetAllProducts();
            // Return the list of products
            return Ok(products);
        }

        // PUT method to update an existing product
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product productToBeUpdated)
        {
            // Call the service to update the product
            _productServices.UpdateProduct(productToBeUpdated);
            // Return success response after update
            return Ok(new { message = "Product updated successfully" });
        }

        // GET method to retrieve a product by its name
        [HttpGet("{name}")]
        public IActionResult GetProductByName(string name)
        {
            // Call the service to get a product by its name
            var product = _productServices.GetProductByName(name);
            // Return not found if no product is found with the given name
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            // Return the found product
            return Ok(product);
        }
    }
}
