using Application.Common.Exceptions;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;
public record DeleteCourseCommand(Guid Id) : IRequest;
public class DeleteCourseCommanHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;

    public DeleteCourseCommanHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .Include(x=>x.CourseImageFile)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (course == null) 
            throw new NotFoundException("Course Not Found");
        try
        {
            await _storageService.DeleteAsync(course.CourseImageFile.Path, course.CourseImageFile.FileName);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        var eduCertificate = await _context.CourseEduCertificates.Where(x => x.CourseId == course.Id).ToListAsync();
        _context.CourseEduCertificates.RemoveRange(eduCertificate);
        _context.Courses.Remove(course);
        await  _context.SaveChangesAsync();
        return Unit.Value;
    }
}