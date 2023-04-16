using Microsoft.EntityFrameworkCore;

namespace GeekShooping.ProductApi.Model.Context
{
    public class MySqlContext:DbContext
    {
        public MySqlContext(){}
        public MySqlContext(DbContextOptions<MySqlContext> oprions):base(oprions){}

        public DbSet<Product> Products { get; set;}
        public DbSet<Category> Category { get; set; }
    }
}
