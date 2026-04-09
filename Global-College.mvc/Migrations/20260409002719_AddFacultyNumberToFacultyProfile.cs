using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyNumberToFacultyProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacultyNumber",
                table: "FacultyProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00001", "faculty00001", "00001@college.com", "00001@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00002", "faculty00002", "00002@college.com", "00002@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00003", "faculty00003", "00003@college.com", "00003@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00004", "faculty00004", "00004@college.com", "00004@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00005", "faculty00005", "00005@college.com", "00005@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00006", "faculty00006", "00006@college.com", "00006@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00007", "faculty00007", "00007@college.com", "00007@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00008", "faculty00008", "00008@college.com", "00008@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00009", "faculty00009", "00009@college.com", "00009@college.com" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FacultyNumber", "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "00010", "faculty00010", "00010@college.com", "00010@college.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacultyNumber",
                table: "FacultyProfiles");

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-1", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-2", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-3", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-4", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-5", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-6", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-7", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-8", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-9", "", "" });

            migrationBuilder.UpdateData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IdentityUserId", "SystemEmail", "SystemPassword" },
                values: new object[] { "faculty-user-10", "", "" });
        }
    }
}
