using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Application.Common.Models;
using EducationWebApi.DataAccess.Common;
using MediatR;

namespace EducationWebApi.Application.Features;

public class GetAllCourseQuery : IRequest<PaginatedList<GetAllCourseDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQuery, PaginatedList<GetAllCourseDto>>
{
    private readonly IDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetAllCourseQueryHandler(IDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetAllCourseDto>> Handle(GetAllCourseQuery query, CancellationToken cancellationToken)
    {
        return await _context.Courses
             .ProjectTo<GetAllCourseDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(query.PageNumber, query.PageSize);
    }
}
