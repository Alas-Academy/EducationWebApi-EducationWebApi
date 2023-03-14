using EducationWebApi.Domain.Common;
namespace EducationWebApi.Core.Entities;

public class CourseEduCertificate 
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid CertificateId { get; set; }
    public EduCertificate Certificate { get; set; }
}
