using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public DeleteModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        public List<Course> InstructorCourses = new List<Course>();

        public IList<Course> Courses { get; set; }

        public IList<Course> studentsInCourse { get; set; }

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
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FindAsync(id);

            if (Course != null)
            {
                Course course = _context.Course.SingleOrDefault(u => u.CourseID.Equals(id));

                int credits = course.Credits;

                if(UserInfo.ID == course.UserInfoID)
                {
                    UserInfo.RegisteredCreditHours += credits;
                    UserInfo.Tuition += (credits * 100);
                }

                _context.Course.Remove(Course);
                await _context.SaveChangesAsync();

                //query database for current Instructor courses
                Courses = await _context.Course
                           .Where(u => u.UserInfoID == UserInfo.ID)
                           .ToListAsync();

                foreach (var item in Courses)
                {
                    InstructorCourses.Add(item);
                }

                //save all current Instructor courses as a Session List
                HttpContext.Session.Remove("InstructorCourses");
                HttpContext.Session.Add<List<Course>>("InstructorCourses", InstructorCourses);
            }

            return RedirectToPage("Index", new { id = HttpContext.Session.GetString("UserId") });
        }
    }
}
