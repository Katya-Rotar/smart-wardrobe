namespace BLL.Services;

using System.Net.Http.Headers;
using System.Text.Json;
using BLL.DTO.ML;

public class MlServiceClient
{
    private readonly HttpClient _httpClient;

    public MlServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MlPredictionResult> PredictImageAsync(Stream fileStream, string fileName, string contentType)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        
        content.Add(fileContent, "file", fileName);
        
        // Змінюємо шлях на /classify (як ми прописали в FastAPI)
        var response = await _httpClient.PostAsync("http://smart-wardrobe.ml.vision:8000/classify", content);
        
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        // Перетворюємо JSON у об'єкт C#
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<MlPredictionResult>(jsonResponse, options);
    }
}