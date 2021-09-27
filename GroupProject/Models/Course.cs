using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationHW1.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        //[ForeignKey("UserInfoID")]
        public int UserInfoID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Instructor First Name")]
        public string InstructorFirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Instructor Last Name")]
        public string InstructorLastName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(6)]
        [Display(Name = "Course Number")]
        public string CourseNumber { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Course Credits")]
        public int Credits { get; set; }

        [Required]
        [Display(Name = "Building Number")]
        public string BuildingName { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Meeting Day One")]
        public string MeetingDayOne { get; set; }


        [Display(Name = "Meeting Day Two")]
        public string MeetingDayTwo { get; set; }


        [Display(Name = "Meeting Day Three")]
        public string MeetingDayThree { get; set; }

        [Required]
        [Display(Name = "Meeting Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        public DateTime MeetingStartTime { get; set; }

        [Required]
        [Display(Name = "Meeting End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss}")]
        public DateTime MeetingEndTime { get; set; }

        public UserInfo UserInfo { get; set; }

        [JsonIgnore]
        public ICollection<StudentRegistration> Registrations { get; set; }

        public ICollection<Assignments> Assignments { get; set; }

        public ICollection<AssignmentSubmissions> AssignmentSubmissions { get; set; }


    }
}
