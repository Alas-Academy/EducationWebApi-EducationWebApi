using EducationWebApi.Domain.Common;

namespace EducationWebApi.Core.Entities;

public class CourseEduCertificate 
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public Guid EduCertificateId { get; set; }
    public EduCertificate EduCertificate { get; set; } = null!;
}
