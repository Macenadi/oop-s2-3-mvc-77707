using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseEnrolmentChangeHistoryAndStoppedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastChangeJustification",
                table: "CourseEnrolments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChangedAt",
                table: "CourseEnrolments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseEnrolmentChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseEnrolmentId = table.Column<int>(type: "int", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrolmentChangeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrolmentChangeHistories_CourseEnrolments_CourseEnrolmentId",
                        column: x => x.CourseEnrolmentId,
                        principalTable: "CourseEnrolments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "LastChangeJustification", "LastChangedAt" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrolmentChangeHistories_CourseEnrolmentId",
                table: "CourseEnrolmentChangeHistories",
                column: "CourseEnrolmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEnrolmentChangeHistories");

            migrationBuilder.DropColumn(
                name: "LastChangeJustification",
                table: "CourseEnrolments");

            migrationBuilder.DropColumn(
                name: "LastChangedAt",
                table: "CourseEnrolments");
        }
    }
}
