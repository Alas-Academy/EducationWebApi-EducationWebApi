using EducationWebApi.DataAccess.Common;
using EducationWebApi.DataAccess.Persistence.Interceptors;
using EducationWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
