using EducationWebApi.Application.Common.Models;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EducationWebApi.Application.Features;

public record CreatePartnerCommand : IRequest<Result>
{
    public string Name { get; init; } = null!;
    public IFormFile Image { get; init; } = null!;
}

public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Result>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;

    public CreatePartnerCommandHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Result> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        (string photoName, string photoPath) = await _storageService.UploadAsync("partners-image", request.Image);
        PartnersImageFile partnerImage = new() { FileName = photoName, Path = photoPath, Storage = _storageService.StorageName };

        await _context.File.AddAsync(partnerImage);
        bool result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while upload photo");

        await _context.Partners.AddAsync(new() { Name = request.Name, PartnersImage = partnerImage });
        result = await _context.SaveChangesAsync(cancellationToken) > 0;
        if (!result) throw new Exception("Something went wrong while create partner");
        return Result.Success();
    }
}