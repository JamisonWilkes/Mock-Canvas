using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationHW1.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        [StringLength(30, ErrorMessage = "Email Address not valid")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password must be at least 8 characters", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password must be at least 8 characters", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "First name is not valid")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Last name is not valid")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MinimumAge(16, ErrorMessage = "Must be 16 years or older to register")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Birthday")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "You must select an account type")]
        public AccountType AccountType { get; set; }

        [StringLength(50)]
        public string StreetAddress { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [StringLength(50)]
        public string Zip { get; set; }

        [Phone]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Url]
        public string Link1 { get; set; }

        [Url]
        public string Link2 { get; set; }

        [Url]
        public string Link3 { get; set; }

        public string ProfileImageFilePath { get; set; }

        public string ImageName { get; set; }

        public int RegisteredCreditHours { get; set; }

        [JsonIgnore]
        public ICollection<Course> Courses { get; set; }

        public ICollection<Assignments> Assignments { get; set; }

        public ICollection<AssignmentSubmissions> AssignmentSubmissions { get; set; }

        [JsonIgnore]
        public ICollection<StudentRegistration> Registrations { get; set; }

        public string StripeID { get; set; }

        public double Tuition { get; set; }



    }

    public enum AccountType
    {
        Student,
        Instructor
    }
}

