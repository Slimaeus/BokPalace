using AutoMapper;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Rooms.Queries;

public static class GetRooms
{
    public sealed record Query : IRequest<IReadOnlyCollection<RoomDto>>;
    sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<RoomDto>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<RoomDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var rooms = await _applicationDbContext.Rooms
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<RoomDto>>(rooms)
                .AsReadOnly();
        }
    }
}
