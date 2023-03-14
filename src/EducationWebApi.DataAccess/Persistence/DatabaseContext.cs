using EducationWebApi.Core.Entities;
using EducationWebApi.Core.Entities.FileModes;
using EducationWebApi.DataAccess.Common;
using EducationWebApi.DataAccess.Persistence.Interceptors;
using EducationWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using File = EducationWebApi.Core.Entities.FileModes.File;

namespace EducationWebApi.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<ApplicationUser, AppRole, Guid>, IDatabaseContext
{
    private readonly IMediatorPublisher _mediatorPublisher;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public DatabaseContext(DbContextOptions<DatabaseContext> options,
        IMediatorPublisher mediatorPublisher,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _mediatorPublisher = mediatorPublisher;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }


    public DbSet<Instructor> Instructors  => Set<Instructor>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentInstructor> StudentInstructors => Set<StudentInstructor>();
    public DbSet<File> File => Set<File>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseImageFile> CourseImages => Set<CourseImageFile>();
    public DbSet<CourseEduCertificate> CourseEduCertificates => Set<CourseEduCertificate>();
    public DbSet<EduCertificate> EduCertificates => Set<EduCertificate>();
    public DbSet<EduCertificateImageFile> EduCertificateImages => Set<EduCertificateImageFile>();
    public DbSet<CourseDetailsTab> CourseDetailsTabs => Set<CourseDetailsTab>();
    public DbSet<Partners> Partners => Set<Partners>();
    public DbSet<SocialMedia> SocialMedias => Set<SocialMedia>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<StudentInstructor>()
            .HasKey(si => new { si.StudentId, si.InstructorId });

        builder.Entity<StudentInstructor>()
            .HasOne(si => si.Student)
            .WithMany(s => s.StudentInstructors)
            .HasForeignKey(si => si.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<StudentInstructor>()
            .HasOne(si => si.Instructor)
            .WithMany(i => i.StudentInstructors)
            .HasForeignKey(si => si.InstructorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Course>().HasOne(a => a.CourseFeatures)
                 .WithOne()
                 .HasForeignKey<CoursesFeature>(a => a.Id)
                 .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CourseEduCertificate>()
           .HasKey(cc => new { cc.CourseId, cc.CertificateId });

        builder.Entity<CourseEduCertificate>()
            .HasOne(cc => cc.Course)
            .WithMany(c => c.CourseEduCertificates)
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CourseEduCertificate>()
            .HasOne(cc => cc.Certificate)
            .WithMany(c => c.CourseEduCertificates)
            .HasForeignKey(cc => cc.CertificateId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediatorPublisher.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }




}
