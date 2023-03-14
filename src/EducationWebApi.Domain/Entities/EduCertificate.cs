using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.Domain.Common;

namespace EducationWebApi.Core.Entities;

public class EduCertificate : BaseAuditableEntity
{
    public EduCertificate()
    {
        CourseEduCertificates = new HashSet<CourseEduCertificate>();
    }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid EduCertificateFileId { get; set; }
    public EduCertificateImageFile EduCertificateFile { get; set; } = null!;
    public ICollection<CourseEduCertificate> CourseEduCertificates { get; set; }
}
