using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }

        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<OrderHeader> Headers { get; set; }
    }
}
