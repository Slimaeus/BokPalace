using AutoMapper;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Domain.Items;
using BokPalace.Domain.Rooms;

namespace BokPalace.Application.Rooms;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>()
            .ForMember(x => x.Id, z => z.MapFrom(y => y.Id.Value));
        CreateMap<Item, ItemDto>();
    }
}
