using CRUD2.Database;
using CRUD2.Controllers;
using CRUD2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // Add MVC controllers with views
builder.Services.AddScoped<ProductDatabase>();      // Add Database service as Scoped (to be injected into controllers)

// Configure your MySQL connection string (adjust the connection string as needed)
builder.Services.AddScoped<MySqlConnection>(provider =>
{
    // Get the connection string from the configuration
    var connectionString = builder.Configuration.GetConnectionString("myConnectionString"); //change the value for the myConnectionString , you will find it in "appsettings.Development.json" ... also you need to change the connection in the ProductDatabase
    return new MySqlConnection(connectionString);  // Return a new MySqlConnection using the connection string
});

// Register ProductServices as Scoped, to be injected into controllers
builder.Services.AddScoped<ProductServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use exception handler for non-development environments
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();  // Enforce HTTP Strict Transport Security for enhanced security
}

// Unfortunately not working as expected yet :(
// TO-DO: Fix the error encountering from the setup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var productDatabase = services.GetRequiredService<ProductDatabase>();  // Get the ProductDatabase service

    try
    {
        // Set up the database and tables if they don't exist
        productDatabase.SetUpDatabase();  // Initialize the database
    }
    catch (Exception ex)
    {
        // Log the exception (this is an example, implement proper logging)
        Console.WriteLine($"An error occurred while setting up the database: {ex.Message}");
    }
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();  // Enable static files (like images, CSS, JS)

app.UseRouting();  // Enable routing for HTTP requests

// Enable authorization (if required)
app.UseAuthorization();  // Apply authorization middleware

// Map the controllers to routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Default route pattern for MVC controllers

app.Run();  // Start the web application
