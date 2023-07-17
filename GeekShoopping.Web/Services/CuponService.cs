using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CuponService : ICuponService
    {
        private readonly HttpClient _client;
        private const string BasePath = "api/v1/cupon";
        public CuponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CuponViewModel> GetCupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.GetAsync($"{BasePath}/{code}");

            if (!response.IsSuccessStatusCode) return new CuponViewModel();

            return await response.ReadContentAs<CuponViewModel>();
        }
    }
}
