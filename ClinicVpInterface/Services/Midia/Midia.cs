using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicVpInterface.Services.Midia
{
    public class Midia
    {

        private readonly HttpClient http = new HttpClient();

        async public Task<string> GetUrlImgProfile(Stream file, string fileName)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            var fileContent = new StreamContent(file);
            formData.Add(fileContent, "file", fileName);

            formData.Add(new StringContent("ml_default"), "upload_preset");

            var url = "https://api.cloudinary.com/v1_1/djdanrpm/image/upload";

            HttpResponseMessage response = await http.PostAsync(url, formData);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            MidiaResponseDto midia = JsonSerializer.Deserialize<MidiaResponseDto>(jsonResponse, options);

            return midia.SecureUrl ?? string.Empty;
        }

    }
}