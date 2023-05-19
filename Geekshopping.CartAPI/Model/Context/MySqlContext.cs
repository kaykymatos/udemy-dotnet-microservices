using Microsoft.EntityFrameworkCore;

namespace Geekshopping.CartAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}
