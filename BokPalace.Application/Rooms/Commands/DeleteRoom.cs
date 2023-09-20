using AutoMapper;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Rooms.Commands;

public static class DeleteRoom
{
    public sealed record Command(RoomId Id) : IRequest<Unit>;
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
            await _applicationDbContext.Rooms
                .Where(x => x.Id.Equals(request.Id))
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
