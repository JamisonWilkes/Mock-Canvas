using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public DetailsModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public Course Course { get; set; }
        public UserInfo UserInfo { get; set; }
        public StudentRegistration StudentRegistration { get; set; }
        public IList<StudentRegistration> studentsRegistered { get; set; }

        public List<Course> Courses = new List<Course>();

        public List<Course> InstructorCourses = new List<Course>();
        public IList<UserInfo> userinfo { get; set; }

        public Assignments assignment { get; set; }
        public AssignmentSubmissions assignmentSubmissions { get; set; }

        public IList<AssignmentSubmissions> assignmentsubmissions { get; set; }
        public IList<Assignments> assignments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseID == id);          

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //If course id is null
            if (id == null)
            {
                return NotFound();
            }
            //Call deleteCourse method, send course id in method call
            bool returnValue = await deleteCourse(id.Value);

            if (!returnValue)
                return NotFound();
            else
            {
                //query database for current Instructor courses
                Courses = _context.Course
                       .Where(u => u.UserInfoID == int.Parse(HttpContext.Session.GetString("UserId")))
                       .ToList();

                //save all current Instructor courses as a Session List
                HttpContext.Session.Remove("InstructorCourses");

                foreach (var item in Courses)
                {
                    InstructorCourses.Add(item);
                }

                HttpContext.Session.Add<List<Course>>("InstructorCourses", InstructorCourses);

                return RedirectToPage("./Index", new { id = int.Parse(HttpContext.Session.GetString("UserId")) });
            }
        }

        public async Task<bool> deleteCourse(int courseID)
        {
            //Get the course info for the currently selected course
            Course = await _context.Course
                            .Include(c => c.UserInfo).FirstOrDefaultAsync(m => m.CourseID == courseID);

            //Get the assignment info for the courses assignments
            assignments = await _context.Assignments.Where(x => x.CourseID == Course.CourseID).ToListAsync();

            assignmentsubmissions = await _context.AssignmentSubmissions.Where(x => x.CourseID == Course.CourseID).ToListAsync();


            //If course is not null then delete the course
            if (Course != null)
            {
                Course course = _context.Course.SingleOrDefault(u => u.CourseID.Equals(courseID));
                studentsRegistered = await _context.CourseRegistrations.Where(x => x.CourseID == course.CourseID).ToListAsync();


                int credits = course.Credits;

                foreach(var item in studentsRegistered)
                {
                    StudentRegistration StudentRegistration = _context.CourseRegistrations.SingleOrDefault(u => u.CourseID.Equals(courseID));
                    userinfo = await _context.UserInfo.Where(x => x.ID == StudentRegistration.UserInfoID).ToListAsync(); 

                    foreach(var thing in userinfo)
                    {
                        thing.RegisteredCreditHours = thing.RegisteredCreditHours - credits;
                        thing.Tuition = thing.Tuition - (credits * 100);
                    }

                }


                if (assignmentsubmissions != null)
                {
                    foreach(var item in assignmentsubmissions)
                    {
                        _context.AssignmentSubmissions.Remove(item);
                    }
                }

                if (assignments != null)
                {
                    foreach (var item in assignments)
                    {
                        _context.Assignments.Remove(item);
                    }
                }


                _context.Course.Remove(Course);
                //Save deletion changes to datbase
                await _context.SaveChangesAsync();
            }
            else
            {
                return false;
            }
            return true;
        }
    }

}
