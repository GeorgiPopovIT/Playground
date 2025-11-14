using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MongoDB_Test;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public static PersonDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<PersonDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public DbSet<Person> Persons { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
