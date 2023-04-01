using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public class GetAllPartnersQuery : IRequest<List<GetAllPartnersQueryDto>> { }

public class GetAllPartnersQueryHandler : IRequestHandler<GetAllPartnersQuery, List<GetAllPartnersQueryDto>>
{
    private readonly IDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetAllPartnersQueryHandler(IDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllPartnersQueryDto>> Handle(GetAllPartnersQuery query, CancellationToken cancellationToken)
    {
        return await _context.Partners
        .Include(i => i.PartnersImage)
        .ProjectTo<GetAllPartnersQueryDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }
}