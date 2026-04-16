using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Global_College.mvc.ViewModels
{
    public class ExamFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateOnly Date { get; set; }

        [Range(0, 1000)]
        public int MaxScore { get; set; }

        public bool ResultsReleased { get; set; }

        [Required]
        [Display(Name = "Class Code")]
        public int BranchCourseId { get; set; }

        [Display(Name = "Course")]
        public string? CourseName { get; set; }

        public IEnumerable<SelectListItem> ClassCodes { get; set; } = new List<SelectListItem>();
    }
}