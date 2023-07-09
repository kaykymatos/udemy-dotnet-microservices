using Geekshopping.CartAPI.Data.ValueObjects;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Geekshopping.CartAPI.Repository
{
    public class CuponRepository : ICuponRepository
    {
        private readonly HttpClient _client;
        private const string BasePath = "api/v1/cupon";
        public CuponRepository(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CuponVO> GetCupon(string cuponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.GetAsync($"{BasePath}/{cuponCode}");
            string content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return new CuponVO();
            return JsonSerializer.Deserialize<CuponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
