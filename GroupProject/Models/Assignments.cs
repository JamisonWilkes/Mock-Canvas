using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationHW1.Models
{
    [Table("Assignments")]
    public class Assignments
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        [Display(Name = "Assignment Title: ")]
        public string AssignmentTitle { get; set; }

        [Required]
        [Display(Name = "Assignment Description: ")]
        public string AssignmentDescription { get; set; }

        [Required]
        [Display(Name = "Points Possible: ")]
        public int AssignmentMaxPoints { get; set; }

        [Required]
        [Display(Name = "Assignment Due Date: ")]
        public DateTime AssignmentDueDate { get; set; }

        [Required]
        [Display(Name = "Submission Type: ")]
        public string FileType { get; set; }

        public int CourseID { get; set; }

        public int UserInfoID { get; set; }

        public UserInfo UserInfo { get; set; }

        public Course Course { get; set; }

        public ICollection<AssignmentSubmissions> AssignmentSubmissions { get; set; }

    }
}
