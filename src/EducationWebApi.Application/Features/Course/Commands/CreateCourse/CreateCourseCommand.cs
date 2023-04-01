using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.Core.Entities;
using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EducationWebApi.Application.Features;

public record CreateCourseCommand : IRequest<Result>
{
    public string Name { get; init; } = null!;
    public string? Icon { get; init; }
    public IFormFile CourseImage { get; init; } = null!;
    public int Modules { get; init; } = 1;
    public string Duration { get; init; } = "1 ay"!;
    public string Language { get; init; } = "Azərbaycanca";
    public int CertificateNumber { get; init; } = 1;
    public CourseDetailsTabDto[] CourseDetails { get; set; } = null!;
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;
    public CreateCourseCommandHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        (string photoName, string photoPath) = await _storageService.UploadAsync("course-image", request.CourseImage);
        CourseImageFile courseImage = new() { FileName = photoName, Path = photoPath, Storage = _storageService.StorageName };

        await _context.File.AddAsync(courseImage);
        bool result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while upload photo");

        CoursesFeature coursesFeature = new()
        {
            Modules = request.Modules,
            Duration = request.Duration,
            Language = request.Language,
            CertificateNumber = request.CertificateNumber,
        };

        Course course = new() { 
        Name = request.Name,
        Icon = request.Icon,
        CourseImageFile = courseImage,
        CourseFeatures = coursesFeature,
        CourseDetails = request.CourseDetails.Select(x => new CourseDetailsTab
        {
            Name = x.Name,
            Type = x.Type,
            Description = x.Description
        }).ToHashSet()
        };

        await _context.Courses.AddAsync(course);
        result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while create course");

        return Result.Success();
    }
}
