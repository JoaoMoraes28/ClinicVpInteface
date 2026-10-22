using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ClinicVpInterface.Services.Patient
{
    public class Patient
    {

        readonly private HttpClient http = new HttpClient();
        readonly private string urlBase = "http://localhost:5166/patient";

        public async Task<(List<PatientResponsePreviewDto>, HttpStatusCode statusCode)> GetPatientsPreview()
        {
            HttpResponseMessage response = await http.GetAsync($"{urlBase}/preview");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var patients = JsonSerializer.Deserialize<List<PatientResponsePreviewDto>>(jsonResponse, options);

            return (patients, response.StatusCode);
        }

        public async Task<(ResponseWriteModel response, HttpStatusCode statusCode)> PostPatient(PatientWriteDto patient)
        {
            var json = JsonSerializer.Serialize(patient);
            var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.PostAsync(urlBase, content);

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

        public async Task<(PatientResponseDto patient, HttpStatusCode statusCode)> GetPatientId(int id)
        {
            HttpResponseMessage response = await http.GetAsync($"{urlBase}/{id}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var patient = JsonSerializer.Deserialize<PatientResponseDto>(jsonResponse, options);

            return (patient, response.StatusCode);
        }
      
        public async Task<HttpStatusCode> DeletePatient(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{urlBase}/{id}");

            HttpResponseMessage response = await http.SendAsync(request);
            return response.StatusCode;
        }
      
        public async Task<(PatientResponseDto, HttpStatusCode)> UpdatePatient(PatientWriteDto updatePatient, int id)
        {
            var json = JsonSerializer.Serialize(updatePatient);
            var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.PutAsync($"{urlBase}/{id}", content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return (null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var newPatient = JsonSerializer.Deserialize<PatientResponseDto>(jsonResponse, options);

            return (newPatient, response.StatusCode);
        }
    }
}