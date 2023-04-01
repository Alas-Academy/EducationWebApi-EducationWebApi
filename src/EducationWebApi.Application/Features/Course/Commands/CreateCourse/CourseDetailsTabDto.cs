
namespace EducationWebApi.Application.Features;

public class CourseDetailsTabDto 
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = "default";
    public string? Description { get; set; }
}
