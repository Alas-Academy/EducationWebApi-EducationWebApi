using EducationWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationWebApi.DataAccess.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly DatabaseContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, DatabaseContext context, UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        try
        {

            AppRole administratorRole = new()
            {
                Name = "Administrator"
            };

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { NameSurname= "Administrator", UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }

            // Seed Instructors
            var instructors = new List<Instructor>
           {
            new Instructor
            {
                UserName = "instructor1@test.com",
                Email = "instructor1@test.com",
                NameSurname = "John Doe",
                Birthday = DateTime.Now.AddYears(-30),
                Gender = Gender.Male
            },
            new Instructor
            {
                UserName = "instructor2@test.com",
                Email = "instructor2@test.com",
                NameSurname = "Jane Smith",
                Birthday = DateTime.Now.AddYears(-35),
                Gender = Gender.Female
            },
            new Instructor
            {
                UserName = "instructor3@test.com",
                Email = "instructor3@test.com",
                NameSurname = "Bob Johnson",
                Birthday = DateTime.Now.AddYears(-40),
                Gender = Gender.Male
            },
            new Instructor
            {
                UserName = "instructor4@test.com",
                Email = "instructor4@test.com",
                NameSurname = "Mary Brown",
                Birthday = DateTime.Now.AddYears(-28),
                Gender = Gender.Female
            },
            new Instructor
            {
                UserName = "instructor5@test.com",
                Email = "instructor5@test.com",
                NameSurname = "Tom Green",
                Birthday = DateTime.Now.AddYears(-32),
                Gender = Gender.Male
            }
        };

            foreach (var instructor in instructors)
            {
                await _userManager.CreateAsync(instructor, "Salam321@");
            }

            // Seed Students
            var students = new List<Student>
        {
            new Student
            {
                UserName = "student1@test.com",
                Email = "student1@test.com",
                NameSurname = "Sarah Lee",
                Birthday = DateTime.Now.AddYears(-22),
                Gender = Gender.Female
            },
            new Student
            {
                UserName = "student2@test.com",
                Email = "student2@test.com",
                NameSurname = "Chris Brown",
                Birthday = DateTime.Now.AddYears(-24),
                Gender = Gender.Male
            },
            new Student
            {
                UserName = "student3@test.com",
                Email = "student3@test.com",
                NameSurname = "Emily Wilson",
                Birthday = DateTime.Now.AddYears(-26),
                Gender = Gender.Female
            },
            new Student
            {
                UserName = "student4@test.com",
                Email = "student4@test.com",
                NameSurname = "Jack Davis",
                Birthday = DateTime.Now.AddYears(-23),
                Gender = Gender.Male
            },
            new Student
            {
                UserName = "student5@test.com",
                Email = "student5@test.com",
                NameSurname = "Lucy Kim",
                Birthday = DateTime.Now.AddYears(-27),
                Gender = Gender.Female
            }
        };

            foreach (var student in students)
            {
                await _userManager.CreateAsync(student, "Password123@");
            }

            // Seed StudentInstructors
            var studentInstructors = new List<StudentInstructor>
            {
            new StudentInstructor
            {
            
            InstructorId = instructors[0].Id,
            StudentId = students[0].Id
            },
            new StudentInstructor
            {
            InstructorId = instructors[0].Id,
            StudentId = students[1].Id
            },
            new StudentInstructor
            {
            InstructorId = instructors[1].Id,
            StudentId = students[2].Id
            },
            new StudentInstructor
            {
            InstructorId = instructors[2].Id,
            StudentId = students[3].Id
            },
            new StudentInstructor
            {
            InstructorId = instructors[3].Id,
            StudentId = students[4].Id
            }
            };
            if (!await _context.StudentInstructors.AnyAsync())
            {
                await _context.StudentInstructors.AddRangeAsync(studentInstructors);
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}
