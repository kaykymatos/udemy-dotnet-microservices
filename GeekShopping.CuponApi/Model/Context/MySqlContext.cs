using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CuponApi.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }
        public DbSet<Cupon> Cupons { get; set; }
    }
}
