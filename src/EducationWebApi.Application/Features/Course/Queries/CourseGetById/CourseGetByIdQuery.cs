using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public record CourseGetByIdQuery(Guid Id) : IRequest<CourseGetByIdQueryDto>;

public class CourseGetByIdQueryHandler : IRequestHandler<CourseGetByIdQuery, CourseGetByIdQueryDto>
{
    private readonly IDatabaseContext _context;
    private readonly IMapper _mapper;

    public CourseGetByIdQueryHandler(IDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CourseGetByIdQueryDto> Handle(CourseGetByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
       .Where(p => p.Id == request.Id)
       .Include(x=>x.CourseImageFile)
       .Include(x=>x.CourseDetails)
       .ProjectTo<CourseGetByIdQueryDto>(_mapper.ConfigurationProvider)
       .FirstOrDefaultAsync(cancellationToken);
        if (course == null)
            throw new NotFoundException("Course Not Found");
        return course;

    }
}