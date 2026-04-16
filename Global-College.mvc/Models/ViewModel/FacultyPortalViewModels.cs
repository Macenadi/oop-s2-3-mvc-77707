using System;
using System.Collections.Generic;

namespace Global_College.mvc.Models.ViewModel
{
    public class FacultyPortalHeaderViewModel
    {
        public string FacultyName { get; set; } = "";
        public string FacultyNumber { get; set; } = "";
        public string SystemEmail { get; set; } = "";
        public int AssignedClassesCount { get; set; }
    }

    public class FacultyPortalClassItemViewModel
    {
        public int BranchCourseId { get; set; }
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }

    public class FacultyMyClassesViewModel
    {
        public FacultyPortalHeaderViewModel Header { get; set; } = new();
        public List<FacultyPortalClassItemViewModel> Items { get; set; } = new();
    }

    public class FacultyPortalAssignmentItemViewModel
    {
        public int AssignmentId { get; set; }
        public int BranchCourseId { get; set; }
        public string Title { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly DueDate { get; set; }
        public int MaxScore { get; set; }
    }

    public class FacultyMyAssignmentsViewModel
    {
        public FacultyPortalHeaderViewModel Header { get; set; } = new();
        public List<FacultyPortalAssignmentItemViewModel> Items { get; set; } = new();
    }

    public class FacultyPortalAttendanceItemViewModel
    {
        public int AttendanceRecordId { get; set; }
        public int BranchCourseId { get; set; }
        public string StudentName { get; set; } = "";
        public string StudentNumber { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly Date { get; set; }
        public bool Present { get; set; }
    }

    public class FacultyMyAttendanceViewModel
    {
        public FacultyPortalHeaderViewModel Header { get; set; } = new();
        public List<FacultyPortalAttendanceItemViewModel> Items { get; set; } = new();
    }

    public class FacultyPortalExamResultItemViewModel
    {
        public int ExamResultId { get; set; }
        public int ExamId { get; set; }
        public int BranchCourseId { get; set; }
        public string ExamTitle { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string StudentNumber { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly Date { get; set; }
        public decimal Score { get; set; }
        public string Grade { get; set; } = "";
        public bool ResultsReleased { get; set; }
    }

    public class FacultyMyExamResultsViewModel
    {
        public FacultyPortalHeaderViewModel Header { get; set; } = new();
        public List<FacultyPortalExamResultItemViewModel> Items { get; set; } = new();
    }
}