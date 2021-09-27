using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationHW1.Models
{
    public class AssignmentSubmissions
    {
        [Key]
        public int SubmissionID { get; set; }

        public bool SubmissionExists { get; set; }

        public string SubmissionFilePath { get; set; }

        public string textEntrySubmission { get; set; }

        [Display(Name = "Submission Name: ")]
        public string SubmissionName { get; set; }

        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Display(Name = "Date and Time of Submission: ")]
        public DateTime TimeOfSubmission { get; set; }

        public int UserInfoID { get; set; }

        public int CourseID { get; set; }

        public int AssignmentID { get; set; }

        [Display(Name = "Grade")]
        public int Grade { get; set; }

        public UserInfo UserInfo { get; set; }

        public Course Course { get; set; }

        public Assignments Assignments { get; set; }


    }
}
