using EducationWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.DataAccess.Common;

public interface IDatabaseContext
{
    public DbSet<Instructor> Instructors { get; }
    public DbSet<Student> Students { get; }
    public DbSet<StudentInstructor> StudentInstructors { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
