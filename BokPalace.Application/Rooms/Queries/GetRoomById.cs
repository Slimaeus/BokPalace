using AutoMapper;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Rooms.Queries;

public static class GetRoomById
{
    public sealed record Query(RoomId Id) : IRequest<RoomDto>;
    sealed class Handler : IRequestHandler<Query, RoomDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var roomDtos = await _applicationDbContext.Rooms
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
                ?? throw new Exception();
            return _mapper.Map<RoomDto>(roomDtos);
        }
    }
}
