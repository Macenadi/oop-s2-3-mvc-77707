using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class SyncPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BranchCourses_BranchId_CourseId",
                table: "BranchCourses");

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_BranchId",
                table: "BranchCourses",
                column: "BranchId");

            migrationBuilder.Sql(@"
            UPDATE BranchCourses
            SET ClassCode = CONCAT('TEMP-', Id)
            WHERE ClassCode IS NULL OR LTRIM(RTRIM(ClassCode)) = ''
            ");

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_ClassCode",
                table: "BranchCourses",
                column: "ClassCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BranchCourses_BranchId",
                table: "BranchCourses");

            migrationBuilder.DropIndex(
                name: "IX_BranchCourses_ClassCode",
                table: "BranchCourses");

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_BranchId_CourseId",
                table: "BranchCourses",
                columns: new[] { "BranchId", "CourseId" },
                unique: true);
        }
    }
}
