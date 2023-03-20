using GeekShopping.CartApi.Data.ValueObjects;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace GeekShopping.CartApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient client;
        public const string BasePath = "api/v1/Coupon";

        public CouponRepository(HttpClient client)
        {           
            this.client = client;
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"{BasePath}/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK) return new CouponVO();
            return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
