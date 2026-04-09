using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddClassCodeAndDatesToBranchCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassCode",
                table: "BranchCourses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "BranchCourses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2026, 12, 31));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "BranchCourses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2026, 1, 1));

            migrationBuilder.Sql(@"
        UPDATE BranchCourses
        SET ClassCode = CONCAT('TEMP-', Id)
        WHERE ClassCode IS NULL OR LTRIM(RTRIM(ClassCode)) = ''
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassCode",
                table: "BranchCourses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BranchCourses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BranchCourses");
        }
    }
}
