namespace Global_College.mvc.Models.ViewModel
{
    public class StudentPortalHeaderViewModel
    {
        public string StudentName { get; set; } = "";
        public string StudentNumber { get; set; } = "";
        public string CurrentCourseName { get; set; } = "";
        public string CurrentBranchName { get; set; } = "";
        public string CurrentClassCode { get; set; } = "";
    }

    public class MyProfileViewModel
    {
        public StudentPortalHeaderViewModel Header { get; set; } = new();
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string SystemEmail { get; set; } = "";
    }

    public class MyEnrolmentItemViewModel
    {
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly EnrolDate { get; set; }
        public string Status { get; set; } = "";
    }

    public class MyEnrolmentsViewModel
    {
        public StudentPortalHeaderViewModel Header { get; set; } = new();
        public List<MyEnrolmentItemViewModel> Items { get; set; } = new();
    }

    public class MyAttendanceItemViewModel
    {
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly Date { get; set; }
        public bool Present { get; set; }
    }

    public class MyAttendanceViewModel
    {
        public StudentPortalHeaderViewModel Header { get; set; } = new();
        public List<MyAttendanceItemViewModel> Items { get; set; } = new();
    }

    public class MyAssignmentItemViewModel
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly DueDate { get; set; }
        public int MaxScore { get; set; }
        public decimal? Score { get; set; }
        public string? Feedback { get; set; }
        public string? Grade { get; set; }
        public string TeacherNames { get; set; } = "";
    }

    public class MyAssignmentsViewModel
    {
        public StudentPortalHeaderViewModel Header { get; set; } = new();
        public List<MyAssignmentItemViewModel> Items { get; set; } = new();
    }

    public class MyExamResultItemViewModel
    {
        public int ExamId { get; set; }
        public string Title { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public DateOnly Date { get; set; }
        public int MaxScore { get; set; }
        public decimal Score { get; set; }
        public string Grade { get; set; } = "";
        public string TeacherNames { get; set; } = "";
    }

    public class MyExamResultsViewModel
    {
        public StudentPortalHeaderViewModel Header { get; set; } = new();
        public List<MyExamResultItemViewModel> Items { get; set; } = new();
    }
}