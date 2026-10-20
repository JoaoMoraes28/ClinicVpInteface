using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicVpInterface.Services.Uf
{
    public class Uf
    {

        readonly private HttpClient http = new HttpClient();

        public async Task<List<UfDto>> GetUfs()
        {
            HttpResponseMessage response = await http.GetAsync("http://localhost:5166/uf");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<UfDto> ufs = JsonSerializer.Deserialize<List<UfDto>>(jsonResponse, options);

            return ufs;
        }

    }
}