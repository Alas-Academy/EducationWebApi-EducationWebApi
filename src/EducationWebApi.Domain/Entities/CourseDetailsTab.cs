using EducationWebApi.Domain.Common;
namespace EducationWebApi.Core.Entities;

public class CourseDetailsTab : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = "default";
    public string? Description { get; set; }
    public Guid CourseId { get; set; } 
    public Course Course { get; set; } = null!;
}
