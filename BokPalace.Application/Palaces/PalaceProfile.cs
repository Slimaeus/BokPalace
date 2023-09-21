using AutoMapper;
using BokPalace.Application.Palaces.Commands;
using BokPalace.Application.Palaces.Dtos;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Domain.Palaces;
using BokPalace.Domain.Rooms;

namespace BokPalace.Application.Palaces;

public class PalaceProfile : Profile
{
    public PalaceProfile()
    {
        CreateMap<Palace, PalaceDto>();
        CreateMap<CreatePalace.Command, Palace>();
        CreateMap<UpdatePalace.Command, Palace>()
            .ForAllMembers(options => options
                .Condition((_, _, srcValue, _) => srcValue is { }));
        CreateMap<Room, PalaceRoomDto>();
    }
}
