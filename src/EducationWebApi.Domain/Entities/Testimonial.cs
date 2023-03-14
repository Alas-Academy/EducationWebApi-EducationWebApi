using EducationWebApi.Domain.Common;
using EducationWebApi.Domain.Entities;

namespace EducationWebApi.Core.Entities;

public class Testimonial : BaseAuditableEntity
{
    public string Content { get; set; } = null!;
    public int Quote { get; set; } = 1;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
