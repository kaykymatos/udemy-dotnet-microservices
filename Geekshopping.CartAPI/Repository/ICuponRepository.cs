
using Geekshopping.CartApi.Data.ValueObjects;

namespace Geekshopping.CartApi.Repository
{
    public interface ICuponRepository
    {
        Task<CuponVO> GetCupon(string cuponCode, string token);
    }
}
