namespace EducationWebApi.Domain.Entities;
public class Student : ApplicationUser
{
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
