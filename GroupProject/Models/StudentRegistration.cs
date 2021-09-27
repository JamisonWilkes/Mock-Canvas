using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationHW1.Models
{
    [Table("StudentRegistration")]
    public class StudentRegistration
    {
        [Key]
        public int RegistrationID { get; set; }

        public int UserInfoID { get; set; }

        public int CourseID { get; set; }

        public double TuitionCost { get; set; }

        public string LetterGrade { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserInfo UserInfo { get; set; }

        public Course Course { get; set; }

    }
}
