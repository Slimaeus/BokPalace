using System.Text;
using System.Text.Json;

namespace BokPalace.Web.Data;

public abstract class BaseApiService
{
    private readonly HttpClient _httpClient;

    public BaseApiService(IHttpClientFactory httpClientFactory)
        => _httpClient = httpClientFactory.CreateClient("BokPalace");
    public async Task<TResult?> GetAsync<TResult>(string route)
        => await DeserilizeContent<TResult>(await _httpClient.GetAsync(route));
    public async Task<TResult?> PostAsync<TResult>(string route, object body)
    {
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");
        return await DeserilizeContent<TResult>(await _httpClient.PostAsync(route, jsonContent));
    }

    private static async Task<TResult?> DeserilizeContent<TResult>(HttpResponseMessage response)
    {
        try
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return await JsonSerializer.DeserializeAsync<TResult>(await response.Content.ReadAsStreamAsync(), serializerOptions);
        }
        catch (Exception)
        {
            return default;
        }
    }
}
