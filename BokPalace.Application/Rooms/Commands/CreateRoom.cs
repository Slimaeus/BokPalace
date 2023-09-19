using AutoMapper;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;

namespace BokPalace.Application.Rooms.Commands;

public static class CreateRoom
{
    public sealed record Command(string Name, string? Description) : IRequest<RoomId>;
    sealed class Handler : IRequestHandler<Command, RoomId>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<RoomId> Handle(Command request, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Room>(request);

            await _applicationDbContext.AddAsync(room, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return room.Id;
        }
    }
}
