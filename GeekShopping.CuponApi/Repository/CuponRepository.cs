using AutoMapper;
using GeekShopping.CuponApi.Model.Context;

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
    }
}
