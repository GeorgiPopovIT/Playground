using ConsoleApp3.MongoDbTest;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace ConsoleApp3;


public class AnimalDbContext : DbContext
{
    public AnimalDbContext()
    {

    }

    public AnimalDbContext(DbContextOptions<AnimalDbContext> options)
        : base(options)
    {

    }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Animal> Animals { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=testJsonColumns;Integrated Security=true;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Customer>().ToCollection("CustomerData");
        builder.Entity<Animal>().OwnsOne(
            animal => animal.Country, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        base.OnModelCreating(builder);
    }


}
