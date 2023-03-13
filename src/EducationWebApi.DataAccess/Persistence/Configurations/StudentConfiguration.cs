//using EducationWebApi.Domain.Entities;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace EducationWebApi.DataAccess.Persistence.Configurations;

//public class StudentConfiguration : IEntityTypeConfiguration<Student>
//{
//    public void Configure(EntityTypeBuilder<Student> builder)
//    {
//        builder.HasMany(s => s.Instructors)
//            .WithMany(i => i.Students)
//            .UsingEntity<StudentInstructor>(
//                j => j.HasOne(s => s.Instructor).WithMany().HasForeignKey(s => s.InstructorId),
//                j => j.HasOne(i => i.Student).WithMany().HasForeignKey(i => i.StudentId),
//                j => j.ToTable("StudentInstructors"));
//    }
//}
