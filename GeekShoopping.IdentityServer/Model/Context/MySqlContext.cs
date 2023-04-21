using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShoopping.IdentityServer.Model.Context
{
    public class MySqlContext : IdentityDbContext<ApplicationUser>
    {
        public MySqlContext()
        {
        }
        public MySqlContext(DbContextOptions<MySqlContext> oprions) : base(oprions) { }

    }
}
