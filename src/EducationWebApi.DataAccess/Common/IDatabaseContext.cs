using EducationWebApi.Core.Entities;
using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = EducationWebApi.Core.Entities.FileModes.File;

namespace EducationWebApi.DataAccess.Common;

public interface IDatabaseContext
{

    public DbSet<Instructor> Instructors { get; }
    public DbSet<Student> Students { get; }
    public DbSet<File> File { get; }
    public DbSet<StudentInstructor> StudentInstructors { get; }
    public DbSet<Course> Courses { get; }
    public DbSet<CourseImageFile> CourseImages { get; }
    public DbSet<CourseEduCertificate> CourseEduCertificates { get; }
    public DbSet<EduCertificate> EduCertificates{ get; }
    public DbSet<EduCertificateImageFile> EduCertificateImages { get; }
    public DbSet<CourseDetailsTab> CourseDetailsTabs { get; }
    public DbSet<Partners> Partners{ get; }
    public DbSet<SocialMedia> SocialMedias { get; }
    public DbSet<Testimonial> Testimonials{ get; }



    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
