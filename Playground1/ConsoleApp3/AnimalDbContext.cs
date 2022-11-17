using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3;


public class AnimalDbContext : IdentityDbContext<User>
{
    public AnimalDbContext()
    {

    }

    public AnimalDbContext(DbContextOptions<AnimalDbContext> options)
        :base(options)
    {

    }

    public DbSet<Animal> Animals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost; Database=dotnet-6-PostgresqlTest;User Id=postgres; Password=12345");

        base.OnConfiguring(optionsBuilder);
    }


}
