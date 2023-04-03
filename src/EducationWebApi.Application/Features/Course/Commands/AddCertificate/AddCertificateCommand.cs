using Application.Common.Exceptions;
using EducationWebApi.Application.Common.Models;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public record AddCertificateCommand : IRequest<Result> 
{ 
    public Guid CourseId { get; init; }
    public Guid CertificateId { get; init; }
}

public class AddCertificateCommandHandler : IRequestHandler<AddCertificateCommand, Result>
{
    private readonly IDatabaseContext _context;

    public AddCertificateCommandHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AddCertificateCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x=>x.Id == request.CourseId);
        var certificate = await _context.EduCertificates.FirstOrDefaultAsync(x => x.Id == request.CertificateId);
        if (course is null) throw new NotFoundException("Course Not Found");
        if(certificate is null) throw new NotFoundException("Certificate Not Found");
        _context.CourseEduCertificates.Add(new()
        {
            EduCertificate = certificate,
            Course = course
        });

        bool result = await _context.SaveChangesAsync(cancellationToken) > 0;
        return result ? Result.Success() : Result.Failure(new[] {"There was a problem while adding"});
    }
}
