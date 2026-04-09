namespace Global_College.domain.Models.Administrator
{
    public class FacultyCourseAssignment
    {
        public int Id { get; set; }

        public int FacultyProfileId { get; set; }
        public FacultyProfile? FacultyProfile { get; set; }

        public int BranchCourseId { get; set; }
        public BranchCourse? BranchCourse { get; set; }
    }
}