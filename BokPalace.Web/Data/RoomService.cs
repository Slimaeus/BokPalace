using System.Text.Json;

namespace BokPalace.Web.Data;

public class RoomService
{
    private readonly HttpClient _httpClient;

    public RoomService(IHttpClientFactory httpClientFactory)
        => _httpClient = httpClientFactory.CreateClient("BokPalace");
    public async Task<RoomDto[]?> GetRoomsAsync()
    {
        var result = await _httpClient.GetAsync("api/rooms");
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        try
        {
            var rooms = await JsonSerializer.DeserializeAsync<RoomDto[]>(await result.Content.ReadAsStreamAsync(), serializerOptions);
            return rooms;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
