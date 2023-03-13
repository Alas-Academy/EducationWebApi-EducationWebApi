
namespace EducationWebApi.Domain.Entities;

public class Instructor : ApplicationUser
{ 
    public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new List<StudentInstructor>();
}
