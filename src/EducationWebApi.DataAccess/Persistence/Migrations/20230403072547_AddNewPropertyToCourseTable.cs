using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationWebApi.DataAccess.Persistence.Migrations
{
    public partial class AddNewPropertyToCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathName",
                table: "Courses");
        }
    }
}
