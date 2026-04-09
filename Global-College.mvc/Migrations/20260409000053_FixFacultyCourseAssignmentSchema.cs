using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class FixFacultyCourseAssignmentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyCourseAssignments_Courses_CourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "FacultyCourseAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyCourseAssignments_Courses_CourseId",
                table: "FacultyCourseAssignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyCourseAssignments_Courses_CourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "FacultyCourseAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyCourseAssignments_Courses_CourseId",
                table: "FacultyCourseAssignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
