using EducationWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.DataAccess.Persistence.Configurations;
public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasMany(i => i.Students)
            .WithMany(s => s.Instructors)
            .UsingEntity<StudentInstructor>(
                j => j.HasOne(i => i.Student).WithMany().HasForeignKey(i => i.StudentId),
                j => j.HasOne(s => s.Instructor).WithMany().HasForeignKey(s => s.InstructorId),
                j => j.ToTable("StudentInstructors"));
    }
}
