using GeekShoopping.Web.Services.IServices;

namespace GeekShoopping.Web.Services
{
    public class CuponService: ICuponService
    {
        private readonly HttpClient _client;
        private const string BasePath = "api/v1/cupon";
        public CuponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
    }
}
