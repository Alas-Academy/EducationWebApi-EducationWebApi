using Application.Common.Exceptions;
using EducationWebApi.Application.Services.Storage;
using EducationWebApi.DataAccess.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Application.Features;

public record DeletePartnerCommand(Guid Id) : IRequest;


public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
{
    private readonly IDatabaseContext _context;
    private readonly IStorageService _storageService;

    public DeletePartnerCommandHandler(IDatabaseContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    public async Task<Unit> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
    {
        var partners = await _context.Partners
             .Include(x => x.PartnersImage)
             .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (partners == null)
            throw new NotFoundException("Partners Not Found");
        try
        {
            await _storageService.DeleteAsync(partners.PartnersImage.Path, partners.PartnersImage.FileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        _context.Partners.Remove(partners);
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}