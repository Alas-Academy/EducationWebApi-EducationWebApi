namespace EducationWebApi.Application.Features;

public class AddCourseDetailDto
{
    public string Name { get; init; } = null!;
    public string Type { get; init; } = "default";
    public string? Description { get; init; }
}
