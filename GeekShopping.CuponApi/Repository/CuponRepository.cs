using AutoMapper;
using GeekShopping.CuponApi.Data.ValueObjects;
using GeekShopping.CuponApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CuponApi.Repository
{
    public class CuponRepository : ICuponRepository
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public CuponRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CuponVO> GetCuponByCuponCode(string cuponCode)
        {
            Model.Cupon cupon = await _context.Cupons.FirstOrDefaultAsync(x => x.CuponCode == cuponCode);
            return _mapper.Map<CuponVO>(cupon);
        }
    }
}
