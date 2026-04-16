using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class LinkAssignmentToBranchCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Assignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BranchCourseId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "AssignmentResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_BranchCourseId",
                table: "Assignments",
                column: "BranchCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_BranchCourses_BranchCourseId",
                table: "Assignments",
                column: "BranchCourseId",
                principalTable: "BranchCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_BranchCourses_BranchCourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_BranchCourseId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "BranchCourseId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "AssignmentResults");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
