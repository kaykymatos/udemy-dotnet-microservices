using GeekShoopping.Web.Models;

namespace GeekShoopping.Web.Services.IServices
{
    public interface ICuponService
    {
        Task<CuponViewModel> GetCupon(string code, string token);
    }
}
