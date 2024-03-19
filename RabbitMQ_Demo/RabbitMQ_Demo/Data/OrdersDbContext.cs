using Microsoft.EntityFrameworkCore;

namespace RabbitMQ_Demo.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> dbContextOptions)
            :base(dbContextOptions) { }


        public DbSet<Order> Orders { get; set; }
    }
}
