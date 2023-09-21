using AutoMapper;
using BokPalace.Application.Palaces.Dtos;
using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BokPalace.Application.Palaces.Queries;

public static class GetPalaceById
{
    public sealed record Query(PalaceId Id) : IRequest<PalaceDto>;
    sealed class Handler : IRequestHandler<Query, PalaceDto>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PalaceDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var palace = await _applicationDbContext.Palaces
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
                ?? throw new Exception();
            return _mapper.Map<PalaceDto>(palace);
        }
    }
}
