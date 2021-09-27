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

namespace WebApplicationHW1.Pages.Registration
{
    public class EditModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public EditModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentRegistration StudentRegistration { get; set; }

        public List<StudentRegistration> currentRegistrations { get; set; }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course
                .Include(c => c.UserInfo).FirstOrDefaultAsync(m => m.CourseID == id);

            if (Course == null)
            {
                return NotFound();
            }
            ViewData["UserInfoID"] = new SelectList(_context.Set<UserInfo>(), "ID", "ConfirmPassword");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int courseID = Int32.Parse(HttpContext.Session.GetString("CourseId"));
            int courseCredits = Int32.Parse(HttpContext.Session.GetString("CourseCredits"));

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            Course course = _context.Course.SingleOrDefault(u => u.CourseID.Equals(courseID));

            int credits = course.Credits;

            CurrentAccount.RegisteredCreditHours += credits;
            CurrentAccount.Tuition += (credits * 100);

            StudentRegistration.UserInfoID = CurrentAccount.ID;
            StudentRegistration.CourseID = courseID;
            StudentRegistration.TuitionCost = courseCredits * 100;
            StudentRegistration.Course = Course;
            StudentRegistration.UserInfo = CurrentAccount;
            StudentRegistration.LastName = CurrentAccount.LastName;
            StudentRegistration.FirstName = CurrentAccount.FirstName;
            _context.CourseRegistrations.Add(StudentRegistration);
            _context.SaveChanges();

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

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Attach(Course).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CourseExists(Course.CourseID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./RegisteredClasses", new { id = CurrentAccount.ID });

        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseID == id);
        }
    }
}
