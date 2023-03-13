namespace EducationWebApi.Domain.Entities;
public class Student : ApplicationUser
{
    public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new List<StudentInstructor>();
}
