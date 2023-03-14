
namespace EducationWebApi.Core.Entities;

public class CoursesFeature
{
    public Guid Id { get; set; }
    public int Modules { get; set; } = 1;
    public string Duration { get; set; } = "1 ay"!;
    public string Language { get; set; } = "Azərbaycanca";
    public int CertificateNumber { get; set; } = 1;
}
