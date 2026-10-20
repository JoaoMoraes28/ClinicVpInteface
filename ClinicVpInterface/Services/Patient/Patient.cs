using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicVpInterface.Services.Patient
{
    public class Patient
    {

        readonly private HttpClient http = new HttpClient();

        public async Task<(ResponseWriteModel response, HttpStatusCode statusCode)> PostPatient(PatientWriteDto patient)
        {
            var json = JsonSerializer.Serialize(patient);
            var contet = new StringContent(json, encoding: Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.PostAsync("http://localhost:5166/patient", contet);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ResponseWriteModel message = new ResponseWriteModel();

                return (message, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            ResponseWriteModel statusPatient = JsonSerializer.Deserialize<ResponseWriteModel>(jsonResponse, options);

            return (statusPatient, response.StatusCode);
        }

    }
}