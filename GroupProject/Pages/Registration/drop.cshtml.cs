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

namespace WebApplicationHW1.Pages.Registration
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public DeleteModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentRegistration RegisteredCourse { get; set; }

        public List<StudentRegistration> currentRegistrations { get; set; }

        public Course Course { get; set; }

        public UserInfo UserInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RegisteredCourse = await _context.CourseRegistrations
                            .Include(c => c.UserInfo).FirstOrDefaultAsync(m => m.CourseID == id);

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            Course course = await _context.Course.SingleOrDefaultAsync(u => u.CourseID.Equals(RegisteredCourse.CourseID));

            

            if (RegisteredCourse != null)
            {
                CurrentAccount.RegisteredCreditHours -= course.Credits;
                CurrentAccount.Tuition -= (course.Credits * 100);
                _context.CourseRegistrations.Remove(RegisteredCourse);
                await _context.SaveChangesAsync();
            }

            //get currently registered courses
            currentRegistrations = _context.CourseRegistrations
                        .Include(c => c.Course)
                        .ThenInclude(c => c.UserInfo)
                        .Where(c => c.UserInfoID == CurrentAccount.ID).ToList();

            //save the currently registered courses to a List<T>
            List<Course> currentCourses = new List<Course>();

            //iterate through the current courses and save them to the list
            foreach (var item in currentRegistrations)
            {
                currentCourses.Add(item.Course);
            }

            //clear the current Session data and add the updated courses
            HttpContext.Session.Remove("CurrentCourses");
            HttpContext.Session.Add<List<Course>>("CurrentCourses", currentCourses);

            return RedirectToPage("./RegisteredClasses", new { id = int.Parse(HttpContext.Session.GetString("UserId")) }) ;
        }
    }
}
