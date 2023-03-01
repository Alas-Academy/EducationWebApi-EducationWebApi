
namespace EducationWebApi.Domain.Entities;

public class StudentInstructor
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public Guid InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;
}
