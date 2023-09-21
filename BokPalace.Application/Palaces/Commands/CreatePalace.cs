using AutoMapper;
using BokPalace.Domain.Palaces;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace BokPalace.Application.Palaces.Commands;

public static class CreatePalace
{
    public sealed record Command(string Name, string? Description) : IRequest<PalaceId>;
    sealed class Handler : IRequestHandler<Command, PalaceId>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PalaceId> Handle(Command request, CancellationToken cancellationToken)
        {
            var palace = _mapper.Map<Palace>(request);

            await _applicationDbContext.AddAsync(palace, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return palace.Id;
        }
    }
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(50);
        }
    }
}
