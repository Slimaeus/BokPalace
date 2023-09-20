using AutoMapper;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;

namespace BokPalace.Application.Rooms.Commands;

public static class UpdateRoom
{
    public sealed record Command(RoomId Id, string Name, string? Description) : IRequest<Unit>;
    sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var room = await _applicationDbContext.Rooms
                .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken)
                ?? throw new Exception();

            _mapper.Map(request, room);

            _applicationDbContext.Rooms
                .Update(room);

            await _applicationDbContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
