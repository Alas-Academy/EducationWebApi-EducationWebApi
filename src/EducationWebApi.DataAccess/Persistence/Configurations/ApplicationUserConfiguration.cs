//using EducationWebApi.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace EducationWebApi.DataAccess.Persistence.Configurations;

//public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
//{
//    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
//    {
//        builder.ToTable("ApplicationUsers");

//        builder.HasKey(x => x.Id);

//        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
//        builder.Property(x => x.NameSurname).HasColumnName("NameSurname").IsRequired();
//        builder.Property(x => x.PhoneNumber).HasColumnName("PhoneNumber");
//        builder.Property(x => x.RefreshToken).HasColumnName("RefreshToken");
//        builder.Property(x => x.Birthday).HasColumnName("Birthday").IsRequired();
//        builder.Property(x => x.Gender).HasColumnName("Gender").IsRequired();
//        builder.Property(x => x.RefreshTokenEndDate).HasColumnName("RefreshTokenEndDate");
//        builder.Property(x => x.ProfilePhotoUrl).HasColumnName("ProfilePhotoUrl");


//        builder.HasMany(s => s.StudentInstructors)
//            .WithOne(si => si.Student)
//            .HasForeignKey(si => si.StudentId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.HasMany(i => i.StudentInstructors)
//            .WithOne(si => si.Instructor)
//            .HasForeignKey(si => si.InstructorId)
//            .OnDelete(DeleteBehavior.Restrict);


//    }
//}
