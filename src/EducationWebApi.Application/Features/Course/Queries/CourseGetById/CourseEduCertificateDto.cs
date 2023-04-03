using AutoMapper;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Application.Helpers;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class CourseEduCertificateDto : IMapFrom<CourseEduCertificate>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CourseEduCertificate, CourseEduCertificateDto>()
            .ForMember(d => d.Image, opt => opt.MapFrom(s => StorageManager.GetUrl(s.EduCertificate.EduCertificateFile.Storage, s.EduCertificate.EduCertificateFile.Path)))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.EduCertificate.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.EduCertificate.Description));
    }
}
