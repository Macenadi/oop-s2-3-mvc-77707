using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddStoppedDateToCourseEnrolment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "StoppedDate",
                table: "CourseEnrolments",
                type: "date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 1,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 2,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 3,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 4,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 5,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 6,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 7,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 8,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 9,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 10,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 11,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 12,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 13,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 14,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 15,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 16,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 17,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 18,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 19,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 20,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 21,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 22,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 23,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 24,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 25,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 26,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 27,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 28,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 29,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 30,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 31,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 32,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 33,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 34,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 35,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 36,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 37,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 38,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 39,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 40,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 41,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 42,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 43,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 44,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 45,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 46,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 47,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 48,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 49,
                column: "StoppedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 50,
                column: "StoppedDate",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoppedDate",
                table: "CourseEnrolments");
        }
    }
}
