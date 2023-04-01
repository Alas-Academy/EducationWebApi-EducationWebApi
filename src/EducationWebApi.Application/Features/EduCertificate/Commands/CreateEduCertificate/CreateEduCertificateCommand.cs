using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EducationWebApi.Application.Features;
public record CreateEduCertificateCommand : IRequest<Result>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IFormFile Image { get; init; } = null!;
}

public class CreateEduCertificateCommandHandler : IRequestHandler<CreateEduCertificateCommand, Result>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;
    public CreateEduCertificateCommandHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Result> Handle(CreateEduCertificateCommand request, CancellationToken cancellationToken)
    {
        (string photoName, string photoPath) = await _storageService.UploadAsync("certificate-image", request.Image);
        EduCertificateImageFile certificateImage = new() { FileName = photoName, Path = photoPath, Storage = _storageService.StorageName };

        await _context.File.AddAsync(certificateImage);
        bool result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while upload photo");



        await _context.EduCertificates.AddAsync(new()
        {
            Name = request.Name,
            Description = request.Description,
            EduCertificateFile = certificateImage
        });
        result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while create certificate");

        return Result.Success();
    }
}