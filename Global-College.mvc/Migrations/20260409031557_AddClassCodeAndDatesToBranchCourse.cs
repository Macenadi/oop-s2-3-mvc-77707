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
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "BranchCourses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });

            migrationBuilder.UpdateData(
                table: "BranchCourses",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClassCode", "EndDate", "StartDate" },
                values: new object[] { "", new DateOnly(1, 1, 1), new DateOnly(1, 1, 1) });
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
