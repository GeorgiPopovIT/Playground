
using Microsoft.EntityFrameworkCore;

namespace Test_CSharp_12;

public class CarDbContext : DbContext
{
    public CarDbContext(){}
    //public CarDbContext(DbContextOptions<CarDbContext> dbContextOptions)
    //    : base(dbContextOptions) { }

    public DbSet<Car> Cars { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=CarDemo;Trusted_Connection=True;;TrustServerCertificate=true");
        base.OnConfiguring(optionsBuilder);
    }
}
