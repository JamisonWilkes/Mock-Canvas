using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public List<Course> InstructorCourses = new List<Course>();

        public IList<Course> Courses { get; set; }

        public CreateModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty]
        public UserInfo UserInfo { get; set; }
        /// <summary>
        /// Method is called when the submit button is pressed
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            //Set variable equal to the currently logged in account
            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            //Call create course method
            createCourse(CurrentAccount.ID, Request.Form["Department"], Request.Form["Day One"], Request.Form["Day Two"], Request.Form["Day Three"],
                HttpContext.Session.GetString("FirstName"), HttpContext.Session.GetString("LastName"));

            //query database for current Instructor courses
            Courses = await _context.Course
                       .Where(u => u.UserInfoID == CurrentAccount.ID)
                       .ToListAsync();

            foreach (var item in Courses)
            {
                InstructorCourses.Add(item);
            }

            //save all current Instructor courses as a Session List
            HttpContext.Session.Add<List<Course>>("InstructorCourses", InstructorCourses);


            //Return course page
            return RedirectToPage("/Courses/Index", new { id = CurrentAccount.ID });

        }
        /// <summary>
        /// Method creates a course 
        /// </summary>
        /// <param name="account"></param>
        public void createCourse(int id, string department, string day1, string day2, string day3, string firstName, string lastName)
        {
            //Set course id equal to instructor id
            Course.UserInfoID = id;
            Course.Department = department;
            Course.MeetingDayOne = day1;
            Course.MeetingDayTwo = day2;
            Course.MeetingDayThree = day3;
            Course.InstructorFirstName = firstName;
            Course.InstructorLastName = lastName;

            /* //Set attributes equal to user inputted values
             Course.Department = Request.Form["Department"];
             Course.MeetingDayOne = Request.Form["Day One"];
             Course.MeetingDayTwo = Request.Form["Day Two"];
             Course.MeetingDayThree = Request.Form["Day Three"];*/

            //Set the first and last name of the course to the logged in instructor
            //Course.InstructorFirstName = HttpContext.Session.GetString("FirstName");
            //Course.InstructorLastName = HttpContext.Session.GetString("LastName");

            //Add the newy made course object and save changes
            _context.Course.Add(Course);
            _context.SaveChanges();

        }
    }
}
