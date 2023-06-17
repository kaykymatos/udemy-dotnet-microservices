using GeekShopping.CuponApi.Data.ValueObjects;

namespace GeekShopping.CuponApi.Repository
{
    public interface ICuponRepository
    {
        Task<CuponVO> GetCuponByCuponCode(string cuponCode);
    }
}
