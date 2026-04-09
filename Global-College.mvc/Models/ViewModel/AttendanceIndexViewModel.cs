using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Global_College.mvc.ViewModels
{
    public class AttendanceIndexViewModel
    {
        public int? BranchId { get; set; }
        public int? CourseId { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public List<SelectListItem> Branches { get; set; } = new();
        public List<SelectListItem> Courses { get; set; } = new();
        public List<AttendanceStudentItemViewModel> Students { get; set; } = new();
    }

    public class AttendanceStudentItemViewModel
    {
        public int CourseEnrolmentId { get; set; }
        public int StudentProfileId { get; set; }
        public string StudentNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool Present { get; set; }
        public int? AttendanceRecordId { get; set; }
    }
}