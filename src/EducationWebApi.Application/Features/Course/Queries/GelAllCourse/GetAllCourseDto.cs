using EducationWebApi.Application.Common.Mappings;
using EducationWebApi.Core.Entities;

namespace EducationWebApi.Application.Features;

public class GetAllCourseDto : IMapFrom<Course>
{
    public Guid Id { get; set; }
    public string PathName { get; set; } = string.Empty;
    public string Name { get; set; } = null!;
    public string? Icon { get; set; }
}
