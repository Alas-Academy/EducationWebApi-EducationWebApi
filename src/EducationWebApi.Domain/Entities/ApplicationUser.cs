using Microsoft.AspNetCore.Identity;

namespace EducationWebApi.Domain.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
    public string NameSurname { get; set; } = null!;
    public override string? PhoneNumber { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public string ProfilePhotoUrl { get; set; } = String.Empty;
    public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new List<StudentInstructor>();
}
