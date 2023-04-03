using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public class GetAllEduCertificateQuery : IRequest<List<GetAllEduCertificateDto>> { }

public class GetAllEduCertificateQueryHandler : IRequestHandler<GetAllEduCertificateQuery, List<GetAllEduCertificateDto>>
{
    private readonly IDatabaseContext _context;
    private readonly IMapper _mapper;

    public GetAllEduCertificateQueryHandler(IDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllEduCertificateDto>> Handle(GetAllEduCertificateQuery query, CancellationToken cancellationToken)
    {
        return await _context.EduCertificates
        .Include(i => i.EduCertificateFile)
        .ProjectTo<GetAllEduCertificateDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }
}




