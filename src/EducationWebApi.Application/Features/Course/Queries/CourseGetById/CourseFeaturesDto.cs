using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class CourseFeatureDto : IMapFrom<CoursesFeature>
{
    public int Modules { get; set; } = 1;
    public string Duration { get; set; } = "1 ay"!;
    public string Language { get; set; } = "Azərbaycanca";
    public int CertificateNumber { get; set; } = 1;
}
