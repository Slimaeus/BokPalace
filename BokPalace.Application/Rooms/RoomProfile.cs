using AutoMapper;
using BokPalace.Application.Rooms.Commands;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Domain.Rooms;
using BokPalace.Domain.ValueObjects;

namespace BokPalace.Application.Rooms;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>();
        CreateMap<CreateRoom.Command, Room>();
        CreateMap<UpdateRoom.Command, Room>()
            .ForAllMembers(options => options
                .Condition((_, _, srcValue, _) => srcValue is { }));
        CreateMap<Item, ItemDto>();
    }
}
