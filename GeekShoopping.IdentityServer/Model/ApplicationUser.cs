using Microsoft.AspNetCore.Identity;

namespace GeekShoopping.IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        private string FisrtName { get; set; }
        private string LastName { get; set; }
    }
}
