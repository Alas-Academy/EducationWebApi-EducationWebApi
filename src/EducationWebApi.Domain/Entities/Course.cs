using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.Domain.Common;

namespace EducationWebApi.Core.Entities;
public class Course : BaseAuditableEntity
{
    public Course()
    {
        CourseFeatures = new();
        CourseEduCertificates = new HashSet<CourseEduCertificate>();
        CourseDetails = new HashSet<CourseDetailsTab>();

    }
    public string Name { get; set; } = null!;
    public string? Icon { get; set; }
    public Guid CourseImageFileId { get; set; }
    public CourseImageFile CourseImageFile { get; set; } = null!;
    public CoursesFeature CourseFeatures { get; set; }
    public ICollection<CourseDetailsTab> CourseDetails { get; set; }
    public ICollection<CourseEduCertificate> CourseEduCertificates { get; set; }
}
