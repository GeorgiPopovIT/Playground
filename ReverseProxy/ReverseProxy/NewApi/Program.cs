var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

// In-memory data stores
var products = new List<Product>
{
	new Product(1, "NEW Laptop", 999.99m, 10),
	new Product(2, " NEW Mouse", 29.99m, 50),
	new Product(3, "NEW Keyboard", 79.99m, 30)
};

var users = new List<User>
{
	new User(1, "NEW John Doe", "john@example.com"),
	new User(2, "NEW Jane Smith", "jane@example.com")
};

app.MapGet("/products", () => Results.Ok(products))
	.WithName("GetProducts");

app.MapGet("/products/{id}", (int id) =>
{
	var product = products.FirstOrDefault(p => p.Id == id);
	return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProduct");

app.MapPost("/products", (ProductDto dto) =>
{
	var newProduct = new Product(
		products.Any() ? products.Max(p => p.Id) + 1 : 1,
		dto.Name,
		dto.Price,
		dto.Stock
	);
	products.Add(newProduct);
	return Results.Created($"/products/{newProduct.Id}", newProduct);
})
.WithName("CreateProduct");

app.MapPut("/products/{id}", (int id, ProductDto dto) =>
{
	var product = products.FirstOrDefault(p => p.Id == id);
	if (product is null)
		return Results.NotFound();

	products.Remove(product);
	var updatedProduct = new Product(id, dto.Name, dto.Price, dto.Stock);
	products.Add(updatedProduct);
	return Results.Ok(updatedProduct);
})
.WithName("UpdateProduct");

app.MapDelete("/products/{id}", (int id) =>
{
	var product = products.FirstOrDefault(p => p.Id == id);
	if (product is null)
		return Results.NotFound();

	products.Remove(product);
	return Results.NoContent();
})
.WithName("DeleteProduct");

// User endpoints
app.MapGet("/users", () => Results.Ok(users))
	.WithName("GetUsers");

app.MapGet("/users/{id}", (int id) =>
{
	var user = users.FirstOrDefault(u => u.Id == id);
	return user is not null ? Results.Ok(user) : Results.NotFound();
})
.WithName("GetUser");

app.MapPost("/users", (UserDto dto) =>
{
	var newUser = new User(
		users.Any() ? users.Max(u => u.Id) + 1 : 1,
		dto.Name,
		dto.Email
	);
	users.Add(newUser);
	return Results.Created($"/users/{newUser.Id}", newUser);
})
.WithName("CreateUser");

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
	.WithName("HealthCheck");

app.Run();

internal record Product(int Id, string Name, decimal Price, int Stock);
internal record ProductDto(string Name, decimal Price, int Stock);

internal record User(int Id, string Name, string Email);
internal record UserDto(string Name, string Email);


