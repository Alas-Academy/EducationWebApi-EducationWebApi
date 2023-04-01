using Application.Common.Exceptions;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public record DeleteEduCertificateCommand(Guid Id) : IRequest;

public class DeleteEduCertificateCommandHandler : IRequestHandler<DeleteEduCertificateCommand>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;

    public DeleteEduCertificateCommandHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Unit> Handle(DeleteEduCertificateCommand request, CancellationToken cancellationToken)
    {
        var certificate = await _context.EduCertificates
             .Include(x => x.EduCertificateFile)
             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (certificate == null)
            throw new NotFoundException("Certificate Not Found");
        try
        {
            await _storageService.DeleteAsync(certificate.EduCertificateFile.Path, certificate.EduCertificateFile.FileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        var courseEdueduCertificate = await _context.CourseEduCertificates.Where(x => x.CourseId == certificate.Id).ToListAsync();
        _context.CourseEduCertificates.RemoveRange(courseEdueduCertificate);
        _context.EduCertificates.Remove(certificate);
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}
