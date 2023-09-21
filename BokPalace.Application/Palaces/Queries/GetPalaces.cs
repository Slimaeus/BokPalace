using AutoMapper;
using BokPalace.Application.Palaces.Dtos;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Palaces.Queries;

public static class GetPalaces
{
    public sealed record Query : IRequest<IReadOnlyCollection<PalaceDto>>;
    sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<PalaceDto>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<PalaceDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var palaces = await _applicationDbContext.Palaces
                .AsNoTracking()
                .Include(x => x.Rooms)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<PalaceDto>>(palaces)
                .AsReadOnly();
        }
    }
}
