using AutoMapper;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Application.Helpers;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class GetAllEduCertificateDto : IMapFrom<EduCertificate>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Image { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EduCertificate, GetAllEduCertificateDto>()
            .ForMember(d => d.Image, opt => opt.MapFrom(s => StorageManager.GetUrl(s.EduCertificateFile.Storage, s.EduCertificateFile.Path)));
    }

}