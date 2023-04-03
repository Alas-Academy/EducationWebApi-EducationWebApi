using EducationWebApi.Core.Entities;
using AutoMapper;
using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Application.Helpers;

namespace EducationWebApi.Application.Features;

public class CourseGetByIdQueryDto : IMapFrom<Course>
{
    public Guid Id { get; set; }
    public string PathName { get; set; } = string.Empty;
    public string Name { get; set; } = null!;
    public string? Icon { get; set; }

    public string Image { get; set; } = null!;
    public CourseFeatureDto? Feature { get; set; }
    public ICollection<CourseDetailsDto>? Details { get; set; }
    public ICollection<CourseEduCertificateDto>? Certificates { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseGetByIdQueryDto>()

            .ForMember(d => d.Feature, opt => opt.MapFrom(s => s.CourseFeatures))

            .ForMember(d => d.Details, opt => opt.MapFrom(s => s.CourseDetails))

             .ForMember(d => d.Certificates, opt => opt.MapFrom(s => s.CourseEduCertificates))

             .ForMember(d => d.Image, opt => opt.MapFrom(s =>
             StorageManager.GetUrl(s.CourseImageFile.Storage,
             s.CourseImageFile.Path)));
    }
}
