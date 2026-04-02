using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Global_College.mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacultyProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchCourses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    ResultsReleased = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyCourseAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    FacultyProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyCourseAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyCourseAssignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyCourseAssignments_FacultyProfiles_FacultyProfileId",
                        column: x => x.FacultyProfileId,
                        principalTable: "FacultyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentResults_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentResults_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrolments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrolDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCourseId = table.Column<int>(type: "int", nullable: false),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEnrolments_BranchCourses_BranchCourseId",
                        column: x => x.BranchCourseId,
                        principalTable: "BranchCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseEnrolments_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResults_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResults_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Present = table.Column<bool>(type: "bit", nullable: false),
                    CourseEnrolmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecords_CourseEnrolments_CourseEnrolmentId",
                        column: x => x.CourseEnrolmentId,
                        principalTable: "CourseEnrolments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Courses",
                columns: new[] { "Id", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 12, 31), "Computer Science", new DateOnly(2025, 1, 1) },
                    { 2, new DateOnly(2025, 12, 31), "Business", new DateOnly(2025, 1, 1) },
                    { 3, new DateOnly(2025, 12, 31), "Engineering", new DateOnly(2025, 1, 1) },
                    { 4, new DateOnly(2025, 12, 31), "Design", new DateOnly(2025, 1, 1) },
                    { 5, new DateOnly(2025, 12, 31), "Marketing", new DateOnly(2025, 1, 1) }
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
                    { 1, "Address 1", "student1@college.com", "Student 1", "student-user-1", "0000001", "S0001" },
                    { 2, "Address 2", "student2@college.com", "Student 2", "student-user-2", "0000002", "S0002" },
                    { 3, "Address 3", "student3@college.com", "Student 3", "student-user-3", "0000003", "S0003" },
                    { 4, "Address 4", "student4@college.com", "Student 4", "student-user-4", "0000004", "S0004" },
                    { 5, "Address 5", "student5@college.com", "Student 5", "student-user-5", "0000005", "S0005" },
                    { 6, "Address 6", "student6@college.com", "Student 6", "student-user-6", "0000006", "S0006" },
                    { 7, "Address 7", "student7@college.com", "Student 7", "student-user-7", "0000007", "S0007" },
                    { 8, "Address 8", "student8@college.com", "Student 8", "student-user-8", "0000008", "S0008" },
                    { 9, "Address 9", "student9@college.com", "Student 9", "student-user-9", "0000009", "S0009" },
                    { 10, "Address 10", "student10@college.com", "Student 10", "student-user-10", "00000010", "S0010" },
                    { 11, "Address 11", "student11@college.com", "Student 11", "student-user-11", "00000011", "S0011" },
                    { 12, "Address 12", "student12@college.com", "Student 12", "student-user-12", "00000012", "S0012" },
                    { 13, "Address 13", "student13@college.com", "Student 13", "student-user-13", "00000013", "S0013" },
                    { 14, "Address 14", "student14@college.com", "Student 14", "student-user-14", "00000014", "S0014" },
                    { 15, "Address 15", "student15@college.com", "Student 15", "student-user-15", "00000015", "S0015" },
                    { 16, "Address 16", "student16@college.com", "Student 16", "student-user-16", "00000016", "S0016" },
                    { 17, "Address 17", "student17@college.com", "Student 17", "student-user-17", "00000017", "S0017" },
                    { 18, "Address 18", "student18@college.com", "Student 18", "student-user-18", "00000018", "S0018" },
                    { 19, "Address 19", "student19@college.com", "Student 19", "student-user-19", "00000019", "S0019" },
                    { 20, "Address 20", "student20@college.com", "Student 20", "student-user-20", "00000020", "S0020" },
                    { 21, "Address 21", "student21@college.com", "Student 21", "student-user-21", "00000021", "S0021" },
                    { 22, "Address 22", "student22@college.com", "Student 22", "student-user-22", "00000022", "S0022" },
                    { 23, "Address 23", "student23@college.com", "Student 23", "student-user-23", "00000023", "S0023" },
                    { 24, "Address 24", "student24@college.com", "Student 24", "student-user-24", "00000024", "S0024" },
                    { 25, "Address 25", "student25@college.com", "Student 25", "student-user-25", "00000025", "S0025" },
                    { 26, "Address 26", "student26@college.com", "Student 26", "student-user-26", "00000026", "S0026" },
                    { 27, "Address 27", "student27@college.com", "Student 27", "student-user-27", "00000027", "S0027" },
                    { 28, "Address 28", "student28@college.com", "Student 28", "student-user-28", "00000028", "S0028" },
                    { 29, "Address 29", "student29@college.com", "Student 29", "student-user-29", "00000029", "S0029" },
                    { 30, "Address 30", "student30@college.com", "Student 30", "student-user-30", "00000030", "S0030" },
                    { 31, "Address 31", "student31@college.com", "Student 31", "student-user-31", "00000031", "S0031" },
                    { 32, "Address 32", "student32@college.com", "Student 32", "student-user-32", "00000032", "S0032" },
                    { 33, "Address 33", "student33@college.com", "Student 33", "student-user-33", "00000033", "S0033" },
                    { 34, "Address 34", "student34@college.com", "Student 34", "student-user-34", "00000034", "S0034" },
                    { 35, "Address 35", "student35@college.com", "Student 35", "student-user-35", "00000035", "S0035" },
                    { 36, "Address 36", "student36@college.com", "Student 36", "student-user-36", "00000036", "S0036" },
                    { 37, "Address 37", "student37@college.com", "Student 37", "student-user-37", "00000037", "S0037" },
                    { 38, "Address 38", "student38@college.com", "Student 38", "student-user-38", "00000038", "S0038" },
                    { 39, "Address 39", "student39@college.com", "Student 39", "student-user-39", "00000039", "S0039" },
                    { 40, "Address 40", "student40@college.com", "Student 40", "student-user-40", "00000040", "S0040" },
                    { 41, "Address 41", "student41@college.com", "Student 41", "student-user-41", "00000041", "S0041" },
                    { 42, "Address 42", "student42@college.com", "Student 42", "student-user-42", "00000042", "S0042" },
                    { 43, "Address 43", "student43@college.com", "Student 43", "student-user-43", "00000043", "S0043" },
                    { 44, "Address 44", "student44@college.com", "Student 44", "student-user-44", "00000044", "S0044" },
                    { 45, "Address 45", "student45@college.com", "Student 45", "student-user-45", "00000045", "S0045" },
                    { 46, "Address 46", "student46@college.com", "Student 46", "student-user-46", "00000046", "S0046" },
                    { 47, "Address 47", "student47@college.com", "Student 47", "student-user-47", "00000047", "S0047" },
                    { 48, "Address 48", "student48@college.com", "Student 48", "student-user-48", "00000048", "S0048" },
                    { 49, "Address 49", "student49@college.com", "Student 49", "student-user-49", "00000049", "S0049" },
                    { 50, "Address 50", "student50@college.com", "Student 50", "student-user-50", "00000050", "S0050" }
                });

            migrationBuilder.InsertData(
                table: "BranchCourses",
                columns: new[] { "Id", "BranchId", "CourseId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 3 },
                    { 5, 3, 4 },
                    { 6, 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrolments",
                columns: new[] { "Id", "BranchCourseId", "EnrolDate", "Status", "StudentProfileId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2025, 1, 1), "Enrolled", 1 },
                    { 2, 2, new DateOnly(2025, 1, 1), "Enrolled", 2 },
                    { 3, 3, new DateOnly(2025, 1, 1), "Enrolled", 3 },
                    { 4, 4, new DateOnly(2025, 1, 1), "Enrolled", 4 },
                    { 5, 5, new DateOnly(2025, 1, 1), "Enrolled", 5 },
                    { 6, 6, new DateOnly(2025, 1, 1), "Enrolled", 6 },
                    { 7, 1, new DateOnly(2025, 1, 1), "Enrolled", 7 },
                    { 8, 2, new DateOnly(2025, 1, 1), "Enrolled", 8 },
                    { 9, 3, new DateOnly(2025, 1, 1), "Enrolled", 9 },
                    { 10, 4, new DateOnly(2025, 1, 1), "Enrolled", 10 },
                    { 11, 5, new DateOnly(2025, 1, 1), "Enrolled", 11 },
                    { 12, 6, new DateOnly(2025, 1, 1), "Enrolled", 12 },
                    { 13, 1, new DateOnly(2025, 1, 1), "Enrolled", 13 },
                    { 14, 2, new DateOnly(2025, 1, 1), "Enrolled", 14 },
                    { 15, 3, new DateOnly(2025, 1, 1), "Enrolled", 15 },
                    { 16, 4, new DateOnly(2025, 1, 1), "Enrolled", 16 },
                    { 17, 5, new DateOnly(2025, 1, 1), "Enrolled", 17 },
                    { 18, 6, new DateOnly(2025, 1, 1), "Enrolled", 18 },
                    { 19, 1, new DateOnly(2025, 1, 1), "Enrolled", 19 },
                    { 20, 2, new DateOnly(2025, 1, 1), "Enrolled", 20 },
                    { 21, 3, new DateOnly(2025, 1, 1), "Enrolled", 21 },
                    { 22, 4, new DateOnly(2025, 1, 1), "Enrolled", 22 },
                    { 23, 5, new DateOnly(2025, 1, 1), "Enrolled", 23 },
                    { 24, 6, new DateOnly(2025, 1, 1), "Enrolled", 24 },
                    { 25, 1, new DateOnly(2025, 1, 1), "Enrolled", 25 },
                    { 26, 2, new DateOnly(2025, 1, 1), "Enrolled", 26 },
                    { 27, 3, new DateOnly(2025, 1, 1), "Enrolled", 27 },
                    { 28, 4, new DateOnly(2025, 1, 1), "Enrolled", 28 },
                    { 29, 5, new DateOnly(2025, 1, 1), "Enrolled", 29 },
                    { 30, 6, new DateOnly(2025, 1, 1), "Enrolled", 30 },
                    { 31, 1, new DateOnly(2025, 1, 1), "Enrolled", 31 },
                    { 32, 2, new DateOnly(2025, 1, 1), "Enrolled", 32 },
                    { 33, 3, new DateOnly(2025, 1, 1), "Enrolled", 33 },
                    { 34, 4, new DateOnly(2025, 1, 1), "Enrolled", 34 },
                    { 35, 5, new DateOnly(2025, 1, 1), "Enrolled", 35 },
                    { 36, 6, new DateOnly(2025, 1, 1), "Enrolled", 36 },
                    { 37, 1, new DateOnly(2025, 1, 1), "Enrolled", 37 },
                    { 38, 2, new DateOnly(2025, 1, 1), "Enrolled", 38 },
                    { 39, 3, new DateOnly(2025, 1, 1), "Enrolled", 39 },
                    { 40, 4, new DateOnly(2025, 1, 1), "Enrolled", 40 },
                    { 41, 5, new DateOnly(2025, 1, 1), "Enrolled", 41 },
                    { 42, 6, new DateOnly(2025, 1, 1), "Enrolled", 42 },
                    { 43, 1, new DateOnly(2025, 1, 1), "Enrolled", 43 },
                    { 44, 2, new DateOnly(2025, 1, 1), "Enrolled", 44 },
                    { 45, 3, new DateOnly(2025, 1, 1), "Enrolled", 45 },
                    { 46, 4, new DateOnly(2025, 1, 1), "Enrolled", 46 },
                    { 47, 5, new DateOnly(2025, 1, 1), "Enrolled", 47 },
                    { 48, 6, new DateOnly(2025, 1, 1), "Enrolled", 48 },
                    { 49, 1, new DateOnly(2025, 1, 1), "Enrolled", 49 },
                    { 50, 2, new DateOnly(2025, 1, 1), "Enrolled", 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentResults_AssignmentId",
                table: "AssignmentResults",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentResults_StudentProfileId",
                table: "AssignmentResults",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_CourseEnrolmentId",
                table: "AttendanceRecords",
                column: "CourseEnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_BranchId_CourseId",
                table: "BranchCourses",
                columns: new[] { "BranchId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchCourses_CourseId",
                table: "BranchCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrolments_BranchCourseId",
                table: "CourseEnrolments",
                column: "BranchCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrolments_StudentProfileId_BranchCourseId",
                table: "CourseEnrolments",
                columns: new[] { "StudentProfileId", "BranchCourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_StudentProfileId",
                table: "ExamResults",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseId",
                table: "Exams",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignments_CourseId",
                table: "FacultyCourseAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyCourseAssignments_FacultyProfileId",
                table: "FacultyCourseAssignments",
                column: "FacultyProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignmentResults");

            migrationBuilder.DropTable(
                name: "AttendanceRecords");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "FacultyCourseAssignments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "CourseEnrolments");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "FacultyProfiles");

            migrationBuilder.DropTable(
                name: "BranchCourses");

            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
