using UrlShortener;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure();


await using var app = builder.Build();

if (app.Environment.IsDevelopment())    
{
}

app.MapGet("/", () => "Hello World!");


app.UseHttpsRedirection();

await app.RunAsync();
