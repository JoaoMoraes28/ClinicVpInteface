using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicVpInterface.Services.Auth
{
    public class Auth
    {
        private readonly HttpClient http = new HttpClient();

        public async Task<(ResponseLogin, HttpStatusCode)> Login(AuthDto credentials)
        {
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");

            var url = "http://localhost:5166/auth/login";

            HttpResponseMessage response = await http.PostAsync(url, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var authorized = JsonSerializer.Deserialize<ResponseLogin>(jsonResponse, options);

            return (authorized, response.StatusCode);
        }
    }
}