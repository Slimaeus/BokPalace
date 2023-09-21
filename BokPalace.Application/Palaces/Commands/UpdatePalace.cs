using AutoMapper;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;

namespace BokPalace.Application.Palaces.Commands;

public static class UpdatePalace
{
    public sealed record Command(PalaceId Id, string Name, string? Description) : IRequest<Unit>;
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
            var palace = await _applicationDbContext.Palaces
                .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken)
                ?? throw new Exception();

            _mapper.Map(request, palace);

            _applicationDbContext.Palaces
                .Update(palace);

            await _applicationDbContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
