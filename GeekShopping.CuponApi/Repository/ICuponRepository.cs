using GeekShopping.CuponApi.Data.ValueObjects;

namespace GeekShopping.CuponApi.Repository
{
    public interface ICuponRepository
    {
        Task<CuponVO> GetCuponByuponCode(string cuponCode);
    }
}
