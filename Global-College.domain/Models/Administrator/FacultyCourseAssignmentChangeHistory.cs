namespace Global_College.domain.Models.Administrator
{
    public class FacultyCourseAssignmentChangeHistory
    {
        public int Id { get; set; }

        public int FacultyProfileId { get; set; }
        public FacultyProfile? FacultyProfile { get; set; }

        public string ActionType { get; set; } = string.Empty; // Added / Removed / Updated

        public string CourseName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;

        public DateOnly? CourseStartDate { get; set; }

        public string Justification { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; }
    }
}