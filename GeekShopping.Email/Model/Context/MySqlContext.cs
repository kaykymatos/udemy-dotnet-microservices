using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }

        public DbSet<EmailLog> Emails { get; set; }
    }
}
