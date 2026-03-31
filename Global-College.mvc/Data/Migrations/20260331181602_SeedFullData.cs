using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Global_College.mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFullData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "City Centre", "Dublin" },
                    { 2, "Main Street", "Cork" },
                    { 3, "West District", "Galway" }
                });

            migrationBuilder.InsertData(
                table: "FacultyProfiles",
                columns: new[] { "Id", "Email", "FullName", "IdentityUserId", "Phone" },
                values: new object[,]
                {
                    { 1, "faculty1@college.com", "Faculty 1", "faculty-user-1", "9999991" },
                    { 2, "faculty2@college.com", "Faculty 2", "faculty-user-2", "9999992" },
                    { 3, "faculty3@college.com", "Faculty 3", "faculty-user-3", "9999993" },
                    { 4, "faculty4@college.com", "Faculty 4", "faculty-user-4", "9999994" },
                    { 5, "faculty5@college.com", "Faculty 5", "faculty-user-5", "9999995" },
                    { 6, "faculty6@college.com", "Faculty 6", "faculty-user-6", "9999996" },
                    { 7, "faculty7@college.com", "Faculty 7", "faculty-user-7", "9999997" },
                    { 8, "faculty8@college.com", "Faculty 8", "faculty-user-8", "9999998" },
                    { 9, "faculty9@college.com", "Faculty 9", "faculty-user-9", "9999999" },
                    { 10, "faculty10@college.com", "Faculty 10", "faculty-user-10", "99999910" }
                });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "Id", "Address", "Email", "FullName", "IdentityUserId", "Phone", "StudentNumber" },
                values: new object[,]
                {
                    { 1, "Address 1", "student1@college.com", "Student 1", "student-user-1", "0000001", "STU001" },
                    { 2, "Address 2", "student2@college.com", "Student 2", "student-user-2", "0000002", "STU002" },
                    { 3, "Address 3", "student3@college.com", "Student 3", "student-user-3", "0000003", "STU003" },
                    { 4, "Address 4", "student4@college.com", "Student 4", "student-user-4", "0000004", "STU004" },
                    { 5, "Address 5", "student5@college.com", "Student 5", "student-user-5", "0000005", "STU005" },
                    { 6, "Address 6", "student6@college.com", "Student 6", "student-user-6", "0000006", "STU006" },
                    { 7, "Address 7", "student7@college.com", "Student 7", "student-user-7", "0000007", "STU007" },
                    { 8, "Address 8", "student8@college.com", "Student 8", "student-user-8", "0000008", "STU008" },
                    { 9, "Address 9", "student9@college.com", "Student 9", "student-user-9", "0000009", "STU009" },
                    { 10, "Address 10", "student10@college.com", "Student 10", "student-user-10", "00000010", "STU010" },
                    { 11, "Address 11", "student11@college.com", "Student 11", "student-user-11", "00000011", "STU011" },
                    { 12, "Address 12", "student12@college.com", "Student 12", "student-user-12", "00000012", "STU012" },
                    { 13, "Address 13", "student13@college.com", "Student 13", "student-user-13", "00000013", "STU013" },
                    { 14, "Address 14", "student14@college.com", "Student 14", "student-user-14", "00000014", "STU014" },
                    { 15, "Address 15", "student15@college.com", "Student 15", "student-user-15", "00000015", "STU015" },
                    { 16, "Address 16", "student16@college.com", "Student 16", "student-user-16", "00000016", "STU016" },
                    { 17, "Address 17", "student17@college.com", "Student 17", "student-user-17", "00000017", "STU017" },
                    { 18, "Address 18", "student18@college.com", "Student 18", "student-user-18", "00000018", "STU018" },
                    { 19, "Address 19", "student19@college.com", "Student 19", "student-user-19", "00000019", "STU019" },
                    { 20, "Address 20", "student20@college.com", "Student 20", "student-user-20", "00000020", "STU020" },
                    { 21, "Address 21", "student21@college.com", "Student 21", "student-user-21", "00000021", "STU021" },
                    { 22, "Address 22", "student22@college.com", "Student 22", "student-user-22", "00000022", "STU022" },
                    { 23, "Address 23", "student23@college.com", "Student 23", "student-user-23", "00000023", "STU023" },
                    { 24, "Address 24", "student24@college.com", "Student 24", "student-user-24", "00000024", "STU024" },
                    { 25, "Address 25", "student25@college.com", "Student 25", "student-user-25", "00000025", "STU025" },
                    { 26, "Address 26", "student26@college.com", "Student 26", "student-user-26", "00000026", "STU026" },
                    { 27, "Address 27", "student27@college.com", "Student 27", "student-user-27", "00000027", "STU027" },
                    { 28, "Address 28", "student28@college.com", "Student 28", "student-user-28", "00000028", "STU028" },
                    { 29, "Address 29", "student29@college.com", "Student 29", "student-user-29", "00000029", "STU029" },
                    { 30, "Address 30", "student30@college.com", "Student 30", "student-user-30", "00000030", "STU030" },
                    { 31, "Address 31", "student31@college.com", "Student 31", "student-user-31", "00000031", "STU031" },
                    { 32, "Address 32", "student32@college.com", "Student 32", "student-user-32", "00000032", "STU032" },
                    { 33, "Address 33", "student33@college.com", "Student 33", "student-user-33", "00000033", "STU033" },
                    { 34, "Address 34", "student34@college.com", "Student 34", "student-user-34", "00000034", "STU034" },
                    { 35, "Address 35", "student35@college.com", "Student 35", "student-user-35", "00000035", "STU035" },
                    { 36, "Address 36", "student36@college.com", "Student 36", "student-user-36", "00000036", "STU036" },
                    { 37, "Address 37", "student37@college.com", "Student 37", "student-user-37", "00000037", "STU037" },
                    { 38, "Address 38", "student38@college.com", "Student 38", "student-user-38", "00000038", "STU038" },
                    { 39, "Address 39", "student39@college.com", "Student 39", "student-user-39", "00000039", "STU039" },
                    { 40, "Address 40", "student40@college.com", "Student 40", "student-user-40", "00000040", "STU040" },
                    { 41, "Address 41", "student41@college.com", "Student 41", "student-user-41", "00000041", "STU041" },
                    { 42, "Address 42", "student42@college.com", "Student 42", "student-user-42", "00000042", "STU042" },
                    { 43, "Address 43", "student43@college.com", "Student 43", "student-user-43", "00000043", "STU043" },
                    { 44, "Address 44", "student44@college.com", "Student 44", "student-user-44", "00000044", "STU044" },
                    { 45, "Address 45", "student45@college.com", "Student 45", "student-user-45", "00000045", "STU045" },
                    { 46, "Address 46", "student46@college.com", "Student 46", "student-user-46", "00000046", "STU046" },
                    { 47, "Address 47", "student47@college.com", "Student 47", "student-user-47", "00000047", "STU047" },
                    { 48, "Address 48", "student48@college.com", "Student 48", "student-user-48", "00000048", "STU048" },
                    { 49, "Address 49", "student49@college.com", "Student 49", "student-user-49", "00000049", "STU049" },
                    { 50, "Address 50", "student50@college.com", "Student 50", "student-user-50", "00000050", "STU050" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "BranchId", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2025, 12, 31), "Computer Science", new DateOnly(2025, 1, 1) },
                    { 2, 1, new DateOnly(2025, 12, 31), "Business", new DateOnly(2025, 1, 1) },
                    { 3, 2, new DateOnly(2025, 12, 31), "Engineering", new DateOnly(2025, 1, 1) },
                    { 4, 3, new DateOnly(2025, 12, 31), "Design", new DateOnly(2025, 1, 1) },
                    { 5, 2, new DateOnly(2025, 12, 31), "Marketing", new DateOnly(2025, 1, 1) }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "CourseId", "DueDate", "MaxScore", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2025, 5, 1), 100, "Assignment 1" },
                    { 2, 2, new DateOnly(2025, 5, 2), 100, "Assignment 2" },
                    { 3, 3, new DateOnly(2025, 5, 3), 100, "Assignment 3" },
                    { 4, 4, new DateOnly(2025, 5, 4), 100, "Assignment 4" },
                    { 5, 5, new DateOnly(2025, 5, 5), 100, "Assignment 5" }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrolments",
                columns: new[] { "Id", "CourseId", "EnrolDate", "Status", "StudentProfileId" },
                values: new object[,]
                {
                    { 1, 2, new DateOnly(2025, 1, 1), "Enrolled", 1 },
                    { 2, 3, new DateOnly(2025, 1, 1), "Enrolled", 2 },
                    { 3, 4, new DateOnly(2025, 1, 1), "Enrolled", 3 },
                    { 4, 5, new DateOnly(2025, 1, 1), "Enrolled", 4 },
                    { 5, 1, new DateOnly(2025, 1, 1), "Enrolled", 5 },
                    { 6, 2, new DateOnly(2025, 1, 1), "Enrolled", 6 },
                    { 7, 3, new DateOnly(2025, 1, 1), "Enrolled", 7 },
                    { 8, 4, new DateOnly(2025, 1, 1), "Enrolled", 8 },
                    { 9, 5, new DateOnly(2025, 1, 1), "Enrolled", 9 },
                    { 10, 1, new DateOnly(2025, 1, 1), "Enrolled", 10 },
                    { 11, 2, new DateOnly(2025, 1, 1), "Enrolled", 11 },
                    { 12, 3, new DateOnly(2025, 1, 1), "Enrolled", 12 },
                    { 13, 4, new DateOnly(2025, 1, 1), "Enrolled", 13 },
                    { 14, 5, new DateOnly(2025, 1, 1), "Enrolled", 14 },
                    { 15, 1, new DateOnly(2025, 1, 1), "Enrolled", 15 },
                    { 16, 2, new DateOnly(2025, 1, 1), "Enrolled", 16 },
                    { 17, 3, new DateOnly(2025, 1, 1), "Enrolled", 17 },
                    { 18, 4, new DateOnly(2025, 1, 1), "Enrolled", 18 },
                    { 19, 5, new DateOnly(2025, 1, 1), "Enrolled", 19 },
                    { 20, 1, new DateOnly(2025, 1, 1), "Enrolled", 20 },
                    { 21, 2, new DateOnly(2025, 1, 1), "Enrolled", 21 },
                    { 22, 3, new DateOnly(2025, 1, 1), "Enrolled", 22 },
                    { 23, 4, new DateOnly(2025, 1, 1), "Enrolled", 23 },
                    { 24, 5, new DateOnly(2025, 1, 1), "Enrolled", 24 },
                    { 25, 1, new DateOnly(2025, 1, 1), "Enrolled", 25 },
                    { 26, 2, new DateOnly(2025, 1, 1), "Enrolled", 26 },
                    { 27, 3, new DateOnly(2025, 1, 1), "Enrolled", 27 },
                    { 28, 4, new DateOnly(2025, 1, 1), "Enrolled", 28 },
                    { 29, 5, new DateOnly(2025, 1, 1), "Enrolled", 29 },
                    { 30, 1, new DateOnly(2025, 1, 1), "Enrolled", 30 },
                    { 31, 2, new DateOnly(2025, 1, 1), "Enrolled", 31 },
                    { 32, 3, new DateOnly(2025, 1, 1), "Enrolled", 32 },
                    { 33, 4, new DateOnly(2025, 1, 1), "Enrolled", 33 },
                    { 34, 5, new DateOnly(2025, 1, 1), "Enrolled", 34 },
                    { 35, 1, new DateOnly(2025, 1, 1), "Enrolled", 35 },
                    { 36, 2, new DateOnly(2025, 1, 1), "Enrolled", 36 },
                    { 37, 3, new DateOnly(2025, 1, 1), "Enrolled", 37 },
                    { 38, 4, new DateOnly(2025, 1, 1), "Enrolled", 38 },
                    { 39, 5, new DateOnly(2025, 1, 1), "Enrolled", 39 },
                    { 40, 1, new DateOnly(2025, 1, 1), "Enrolled", 40 },
                    { 41, 2, new DateOnly(2025, 1, 1), "Enrolled", 41 },
                    { 42, 3, new DateOnly(2025, 1, 1), "Enrolled", 42 },
                    { 43, 4, new DateOnly(2025, 1, 1), "Enrolled", 43 },
                    { 44, 5, new DateOnly(2025, 1, 1), "Enrolled", 44 },
                    { 45, 1, new DateOnly(2025, 1, 1), "Enrolled", 45 },
                    { 46, 2, new DateOnly(2025, 1, 1), "Enrolled", 46 },
                    { 47, 3, new DateOnly(2025, 1, 1), "Enrolled", 47 },
                    { 48, 4, new DateOnly(2025, 1, 1), "Enrolled", 48 },
                    { 49, 5, new DateOnly(2025, 1, 1), "Enrolled", 49 },
                    { 50, 1, new DateOnly(2025, 1, 1), "Enrolled", 50 }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CourseId", "Date", "MaxScore", "ResultsReleased", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2025, 6, 1), 100, true, "Exam 1" },
                    { 2, 2, new DateOnly(2025, 6, 2), 100, true, "Exam 2" },
                    { 3, 3, new DateOnly(2025, 6, 3), 100, true, "Exam 3" },
                    { 4, 4, new DateOnly(2025, 6, 4), 100, true, "Exam 4" },
                    { 5, 5, new DateOnly(2025, 6, 5), 100, true, "Exam 5" }
                });

            migrationBuilder.InsertData(
                table: "FacultyCourseAssignments",
                columns: new[] { "Id", "CourseId", "FacultyProfileId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 2 },
                    { 3, 4, 3 },
                    { 4, 5, 4 },
                    { 5, 1, 5 },
                    { 6, 2, 6 },
                    { 7, 3, 7 },
                    { 8, 4, 8 },
                    { 9, 5, 9 },
                    { 10, 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "AssignmentResults",
                columns: new[] { "Id", "AssignmentId", "Feedback", "Score", "StudentProfileId" },
                values: new object[,]
                {
                    { 1, 2, "Good work", 75, 1 },
                    { 2, 3, "Good work", 75, 2 },
                    { 3, 4, "Good work", 75, 3 },
                    { 4, 5, "Good work", 75, 4 },
                    { 5, 1, "Good work", 75, 5 },
                    { 6, 2, "Good work", 75, 6 },
                    { 7, 3, "Good work", 75, 7 },
                    { 8, 4, "Good work", 75, 8 },
                    { 9, 5, "Good work", 75, 9 },
                    { 10, 1, "Good work", 75, 10 },
                    { 11, 2, "Good work", 75, 11 },
                    { 12, 3, "Good work", 75, 12 },
                    { 13, 4, "Good work", 75, 13 },
                    { 14, 5, "Good work", 75, 14 },
                    { 15, 1, "Good work", 75, 15 },
                    { 16, 2, "Good work", 75, 16 },
                    { 17, 3, "Good work", 75, 17 },
                    { 18, 4, "Good work", 75, 18 },
                    { 19, 5, "Good work", 75, 19 },
                    { 20, 1, "Good work", 75, 20 },
                    { 21, 2, "Good work", 75, 21 },
                    { 22, 3, "Good work", 75, 22 },
                    { 23, 4, "Good work", 75, 23 },
                    { 24, 5, "Good work", 75, 24 },
                    { 25, 1, "Good work", 75, 25 },
                    { 26, 2, "Good work", 75, 26 },
                    { 27, 3, "Good work", 75, 27 },
                    { 28, 4, "Good work", 75, 28 },
                    { 29, 5, "Good work", 75, 29 },
                    { 30, 1, "Good work", 75, 30 },
                    { 31, 2, "Good work", 75, 31 },
                    { 32, 3, "Good work", 75, 32 },
                    { 33, 4, "Good work", 75, 33 },
                    { 34, 5, "Good work", 75, 34 },
                    { 35, 1, "Good work", 75, 35 },
                    { 36, 2, "Good work", 75, 36 },
                    { 37, 3, "Good work", 75, 37 },
                    { 38, 4, "Good work", 75, 38 },
                    { 39, 5, "Good work", 75, 39 },
                    { 40, 1, "Good work", 75, 40 },
                    { 41, 2, "Good work", 75, 41 },
                    { 42, 3, "Good work", 75, 42 },
                    { 43, 4, "Good work", 75, 43 },
                    { 44, 5, "Good work", 75, 44 },
                    { 45, 1, "Good work", 75, 45 },
                    { 46, 2, "Good work", 75, 46 },
                    { 47, 3, "Good work", 75, 47 },
                    { 48, 4, "Good work", 75, 48 },
                    { 49, 5, "Good work", 75, 49 },
                    { 50, 1, "Good work", 75, 50 }
                });

            migrationBuilder.InsertData(
                table: "AttendanceRecords",
                columns: new[] { "Id", "CourseEnrolmentId", "Date", "Present" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2025, 2, 1), true },
                    { 2, 2, new DateOnly(2025, 2, 1), true },
                    { 3, 3, new DateOnly(2025, 2, 1), true },
                    { 4, 4, new DateOnly(2025, 2, 1), true },
                    { 5, 5, new DateOnly(2025, 2, 1), true },
                    { 6, 6, new DateOnly(2025, 2, 1), true },
                    { 7, 7, new DateOnly(2025, 2, 1), true },
                    { 8, 8, new DateOnly(2025, 2, 1), true },
                    { 9, 9, new DateOnly(2025, 2, 1), true },
                    { 10, 10, new DateOnly(2025, 2, 1), true },
                    { 11, 11, new DateOnly(2025, 2, 1), true },
                    { 12, 12, new DateOnly(2025, 2, 1), true },
                    { 13, 13, new DateOnly(2025, 2, 1), true },
                    { 14, 14, new DateOnly(2025, 2, 1), true },
                    { 15, 15, new DateOnly(2025, 2, 1), true },
                    { 16, 16, new DateOnly(2025, 2, 1), true },
                    { 17, 17, new DateOnly(2025, 2, 1), true },
                    { 18, 18, new DateOnly(2025, 2, 1), true },
                    { 19, 19, new DateOnly(2025, 2, 1), true },
                    { 20, 20, new DateOnly(2025, 2, 1), true },
                    { 21, 21, new DateOnly(2025, 2, 1), true },
                    { 22, 22, new DateOnly(2025, 2, 1), true },
                    { 23, 23, new DateOnly(2025, 2, 1), true },
                    { 24, 24, new DateOnly(2025, 2, 1), true },
                    { 25, 25, new DateOnly(2025, 2, 1), true },
                    { 26, 26, new DateOnly(2025, 2, 1), true },
                    { 27, 27, new DateOnly(2025, 2, 1), true },
                    { 28, 28, new DateOnly(2025, 2, 1), true },
                    { 29, 29, new DateOnly(2025, 2, 1), true },
                    { 30, 30, new DateOnly(2025, 2, 1), true },
                    { 31, 31, new DateOnly(2025, 2, 1), true },
                    { 32, 32, new DateOnly(2025, 2, 1), true },
                    { 33, 33, new DateOnly(2025, 2, 1), true },
                    { 34, 34, new DateOnly(2025, 2, 1), true },
                    { 35, 35, new DateOnly(2025, 2, 1), true },
                    { 36, 36, new DateOnly(2025, 2, 1), true },
                    { 37, 37, new DateOnly(2025, 2, 1), true },
                    { 38, 38, new DateOnly(2025, 2, 1), true },
                    { 39, 39, new DateOnly(2025, 2, 1), true },
                    { 40, 40, new DateOnly(2025, 2, 1), true },
                    { 41, 41, new DateOnly(2025, 2, 1), true },
                    { 42, 42, new DateOnly(2025, 2, 1), true },
                    { 43, 43, new DateOnly(2025, 2, 1), true },
                    { 44, 44, new DateOnly(2025, 2, 1), true },
                    { 45, 45, new DateOnly(2025, 2, 1), true },
                    { 46, 46, new DateOnly(2025, 2, 1), true },
                    { 47, 47, new DateOnly(2025, 2, 1), true },
                    { 48, 48, new DateOnly(2025, 2, 1), true },
                    { 49, 49, new DateOnly(2025, 2, 1), true },
                    { 50, 50, new DateOnly(2025, 2, 1), true }
                });

            migrationBuilder.InsertData(
                table: "ExamResults",
                columns: new[] { "Id", "ExamId", "Grade", "Score", "StudentProfileId" },
                values: new object[,]
                {
                    { 1, 2, "B", 70, 1 },
                    { 2, 3, "B", 70, 2 },
                    { 3, 4, "B", 70, 3 },
                    { 4, 5, "B", 70, 4 },
                    { 5, 1, "B", 70, 5 },
                    { 6, 2, "B", 70, 6 },
                    { 7, 3, "B", 70, 7 },
                    { 8, 4, "B", 70, 8 },
                    { 9, 5, "B", 70, 9 },
                    { 10, 1, "B", 70, 10 },
                    { 11, 2, "B", 70, 11 },
                    { 12, 3, "B", 70, 12 },
                    { 13, 4, "B", 70, 13 },
                    { 14, 5, "B", 70, 14 },
                    { 15, 1, "B", 70, 15 },
                    { 16, 2, "B", 70, 16 },
                    { 17, 3, "B", 70, 17 },
                    { 18, 4, "B", 70, 18 },
                    { 19, 5, "B", 70, 19 },
                    { 20, 1, "B", 70, 20 },
                    { 21, 2, "B", 70, 21 },
                    { 22, 3, "B", 70, 22 },
                    { 23, 4, "B", 70, 23 },
                    { 24, 5, "B", 70, 24 },
                    { 25, 1, "B", 70, 25 },
                    { 26, 2, "B", 70, 26 },
                    { 27, 3, "B", 70, 27 },
                    { 28, 4, "B", 70, 28 },
                    { 29, 5, "B", 70, 29 },
                    { 30, 1, "B", 70, 30 },
                    { 31, 2, "B", 70, 31 },
                    { 32, 3, "B", 70, 32 },
                    { 33, 4, "B", 70, 33 },
                    { 34, 5, "B", 70, 34 },
                    { 35, 1, "B", 70, 35 },
                    { 36, 2, "B", 70, 36 },
                    { 37, 3, "B", 70, 37 },
                    { 38, 4, "B", 70, 38 },
                    { 39, 5, "B", 70, 39 },
                    { 40, 1, "B", 70, 40 },
                    { 41, 2, "B", 70, 41 },
                    { 42, 3, "B", 70, 42 },
                    { 43, 4, "B", 70, 43 },
                    { 44, 5, "B", 70, 44 },
                    { 45, 1, "B", 70, 45 },
                    { 46, 2, "B", 70, 46 },
                    { 47, 3, "B", 70, 47 },
                    { 48, 4, "B", 70, 48 },
                    { 49, 5, "B", 70, 49 },
                    { 50, 1, "B", 70, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AssignmentResults",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AttendanceRecords",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ExamResults",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FacultyCourseAssignments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "CourseEnrolments",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FacultyProfiles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "StudentProfiles",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
