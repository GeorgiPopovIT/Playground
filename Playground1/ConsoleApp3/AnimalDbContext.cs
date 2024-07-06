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


    public DbSet<Customer> Customers { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=testJsonColumns;IntegratedSecurity=true");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>().ToCollection("CustomerData");

        base.OnModelCreating(builder);
    }


}
