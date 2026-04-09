using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacultyProfilesForMultiCourseSameCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyCourseAssignments_FacultyProfiles_FacultyProfileId",
                table: "FacultyCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_FacultyCourseAssignments_FacultyProfileId",
                table: "FacultyCourseAssignments");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "FacultyProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SystemEmail",
                table: "FacultyProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemPassword",
                table: "FacultyProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchCourseId",
                table: "FacultyCourseAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignments_BranchCourseId",
                table: "FacultyCourseAssignments",
                column: "BranchCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignments_FacultyProfileId_BranchCourseId",
                table: "FacultyCourseAssignments",
                columns: new[] { "FacultyProfileId", "BranchCourseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyCourseAssignments_BranchCourses_BranchCourseId",
                table: "FacultyCourseAssignments",
                column: "BranchCourseId",
                principalTable: "BranchCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyCourseAssignments_FacultyProfiles_FacultyProfileId",
                table: "FacultyCourseAssignments",
                column: "FacultyProfileId",
                principalTable: "FacultyProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyCourseAssignments_BranchCourses_BranchCourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyCourseAssignments_FacultyProfiles_FacultyProfileId",
                table: "FacultyCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_FacultyCourseAssignments_BranchCourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_FacultyCourseAssignments_FacultyProfileId_BranchCourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.DropColumn(
                name: "SystemEmail",
                table: "FacultyProfiles");

            migrationBuilder.DropColumn(
                name: "SystemPassword",
                table: "FacultyProfiles");

            migrationBuilder.DropColumn(
                name: "BranchCourseId",
                table: "FacultyCourseAssignments");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "FacultyProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignments_FacultyProfileId",
                table: "FacultyCourseAssignments",
                column: "FacultyProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyCourseAssignments_FacultyProfiles_FacultyProfileId",
                table: "FacultyCourseAssignments",
                column: "FacultyProfileId",
                principalTable: "FacultyProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
