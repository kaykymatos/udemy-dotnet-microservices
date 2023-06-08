using GeekShopping.CuponApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CuponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponRepository _repository;
        public CuponController(ICuponRepository repository)
        {
            _repository = repository;
        }
    }
}
