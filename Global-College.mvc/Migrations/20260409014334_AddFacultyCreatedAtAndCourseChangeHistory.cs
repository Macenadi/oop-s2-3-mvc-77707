using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyCreatedAtAndCourseChangeHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FacultyProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FacultyCourseAssignmentChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyProfileId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyCourseAssignmentChangeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyCourseAssignmentChangeHistories_FacultyProfiles_FacultyProfileId",
                        column: x => x.FacultyProfileId,
                        principalTable: "FacultyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 840, DateTimeKind.Local).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9796));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9803));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9822));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9827));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 2, 43, 32, 845, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignmentChangeHistories_FacultyProfileId",
                table: "FacultyCourseAssignmentChangeHistories",
                column: "FacultyProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyCourseAssignmentChangeHistories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FacultyProfiles");
        }
    }
}
