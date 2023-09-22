namespace BokPalace.Web.Data;

public class RoomService : BaseApiService
{
    public RoomService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }
    public Task<RoomDto[]?> GetRoomsAsync()
        => GetAsync<RoomDto[]?>("api/rooms");
    public Task<RoomDto?> CreateRoomAsync(CreateRoomRequest request)
        => PostAsync<RoomDto?>("api/rooms", request);
}
