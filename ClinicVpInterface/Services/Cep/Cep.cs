using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicVpInterface.Services.Cep
{
    public class Cep
    {

        readonly private HttpClient http = new HttpClient();

        public async Task<(CepDto, HttpStatusCode)> GetAddressByCep(string cep)
        {
            try
            {
                HttpResponseMessage response = await http.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                CepDto address = JsonSerializer.Deserialize<CepDto>(jsonResponse, options);

                return (address, response.StatusCode);
            }
            catch
            {
                return (null, HttpStatusCode.NotFound);
            }
        }

    }
}