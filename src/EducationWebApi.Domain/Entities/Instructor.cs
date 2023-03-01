
namespace EducationWebApi.Domain.Entities;

public class Instructor : ApplicationUser
{
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
