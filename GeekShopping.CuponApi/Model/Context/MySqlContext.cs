using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CuponApi.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cupon>().HasData(new Cupon
            {
                Id = 1,
                CuponCode = "TESTECUPON",
                DiscountAmount = 10
            });

            modelBuilder.Entity<Cupon>().HasData(new Cupon
            {
                Id = 2,
                CuponCode = "TESTECUPON2023",
                DiscountAmount = 15
            });
        }
        public DbSet<Cupon> Cupons { get; set; }
    }
}
