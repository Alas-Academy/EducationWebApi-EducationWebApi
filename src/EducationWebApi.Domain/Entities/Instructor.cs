
namespace EducationWebApi.Domain.Entities;

public class Instructor : ApplicationUser
{
    public string? Designation { get; set; }
    public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new List<StudentInstructor>();
}
