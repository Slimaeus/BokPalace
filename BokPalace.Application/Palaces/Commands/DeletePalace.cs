using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Palaces.Commands;

public static class DeletePalace
{
    public sealed record Command(PalaceId Id) : IRequest<Unit>;
    sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Handler(ApplicationDbContext applicationDbContext)
            => _applicationDbContext = applicationDbContext;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _applicationDbContext.Palaces
                .Where(x => x.Id.Equals(request.Id))
                .ExecuteDeleteAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
