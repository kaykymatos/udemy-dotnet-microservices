using GeekShopping.CuponApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CuponApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly ICuponRepository _repository;
        public CuponController(ICuponRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{cuponCode}")]
        public async Task<IActionResult> GetCuponByCuponCode([FromRoute] string cuponCode)
        {
            Data.ValueObjects.CuponVO cupon = await _repository.GetCuponByCuponCode(cuponCode);
            if (cupon == null) return NotFound();
            return Ok(cupon);
        }
    }
}
