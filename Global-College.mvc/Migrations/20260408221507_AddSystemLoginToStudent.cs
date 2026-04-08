using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemLoginToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemEmail",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemPassword",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "SystemEmail", "SystemPassword" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemEmail",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "SystemPassword",
                table: "StudentProfiles");
        }
    }
}
