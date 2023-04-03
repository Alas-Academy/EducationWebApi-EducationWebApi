using Application.Common.Exceptions;
using EducationWebApi.Application.Common.Models;
using EducationWebApi.Core.Entities;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public  record AddCourseDetailCommand : IRequest<Result>
{
    public Guid CourseId { get; init; }
    public List<AddCourseDetailDto> Detail { get; init; } = new();
}

public class AddDetailCommandHandler : IRequestHandler<AddCourseDetailCommand, Result>
{
    private readonly IDatabaseContext _context;

    public AddDetailCommandHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddCourseDetailCommand request, CancellationToken cancellationToken)
    {
        var  course = await _context.Courses
            .Include(x=>x.CourseDetails)
            .FirstOrDefaultAsync(x => x.Id == request.CourseId);
        if (course is null)
            throw new NotFoundException("Course Not Found");

        await _context.CourseDetailsTabs.AddRangeAsync(request.Detail.Select(x => new CourseDetailsTab
        {
            Name = x.Name,
            Type = x.Type,
            Description = x.Description,
            Course = course
        }));
        bool result = await _context.SaveChangesAsync() > 0;
        return result ? Result.Success() : Result.Failure(new[] { "Error occurred while adding details" });
    }
}
