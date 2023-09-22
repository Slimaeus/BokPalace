namespace BokPalace.Web.Data;

public class RoomService : BaseApiService
{
    public RoomService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }
    public Task<RoomDto[]?> GetRoomsAsync()
        => GetAsync<RoomDto[]?>("api/rooms");
}
