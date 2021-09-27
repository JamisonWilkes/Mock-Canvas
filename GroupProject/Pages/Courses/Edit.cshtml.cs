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
    public class EditModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public EditModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public IList<StudentRegistration> studentsRegistered { get; set; }
        public IList<UserInfo> userinfo { get; set; }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Course currentCourse = _context.Course.SingleOrDefault(m => m.CourseID == id.Value);
            studentsRegistered = await _context.CourseRegistrations.Where(x => x.CourseID == Course.CourseID).ToListAsync();



            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            //foreach (var error in errors)
            //{
            //    var n = errors;
            //}

            /*           if (!ModelState.IsValid)
                       {
                           return Page();
                       }*/

            /*            _context.Attach(Course).State = EntityState.Modified;*/
            // _context.Attach(userInfo).State = EntityState.Modified;


            if (Course.Department != null)
            {
                currentCourse.Department = Course.Department;
            }
            else
            {
                currentCourse.Department = currentCourse.Department;
            }

            if (Course.CourseTitle != null)
            {
                currentCourse.CourseTitle = Course.CourseTitle;
            }
            else
            {
                currentCourse.CourseTitle = currentCourse.CourseTitle;
            }

            if (Course.CourseNumber != null)
            {
                currentCourse.CourseNumber = Course.CourseNumber;
            }
            else
            {
                currentCourse.CourseNumber = currentCourse.CourseNumber;
            }

            if (Course.CourseDescription != null)
            {
                currentCourse.CourseDescription = Course.CourseDescription;
            }
            else
            {
                currentCourse.CourseDescription = currentCourse.CourseDescription;
            }

            if (Course.Credits.ToString() != null)
            {
                int originalCredit = currentCourse.Credits;
                currentCourse.Credits = Course.Credits;
                int credits = Course.Credits;
                foreach (var item in studentsRegistered)
                {
                    StudentRegistration StudentRegistration = _context.CourseRegistrations.SingleOrDefault(u => u.CourseID.Equals(id));
                    userinfo = await _context.UserInfo.Where(x => x.ID == StudentRegistration.UserInfoID).ToListAsync();

                    foreach (var thing in userinfo)
                    {
                        thing.RegisteredCreditHours = (thing.RegisteredCreditHours - originalCredit) + credits;
                        thing.Tuition = thing.Tuition - (originalCredit * 100) + (credits * 100);
                    }

                }
            }
            else
            {
                currentCourse.Credits = currentCourse.Credits;
            }

            if (Course.RoomNumber != null)
            {
                currentCourse.RoomNumber = Course.RoomNumber;
            }
            else
            {
                currentCourse.RoomNumber = currentCourse.RoomNumber;
            }

            if (Course.BuildingName != null)
            {
                currentCourse.BuildingName = Course.BuildingName;
            }
            else
            {
                currentCourse.BuildingName = currentCourse.BuildingName;
            }

            if (Course.MeetingDayOne != null)
            {
                currentCourse.MeetingDayOne = Course.MeetingDayOne;
            }
            else
            {
                currentCourse.MeetingDayOne = currentCourse.MeetingDayOne;
            }

            if (Course.MeetingDayTwo != null)
            {
                currentCourse.MeetingDayTwo = Course.MeetingDayTwo;
            }
            else
            {
                currentCourse.MeetingDayTwo = currentCourse.MeetingDayTwo;
            }

            if (Course.MeetingDayThree != null)
            {
                currentCourse.MeetingDayThree = Course.MeetingDayThree;
            }
            else
            {
                currentCourse.MeetingDayThree = currentCourse.MeetingDayThree;
            }

            if (Course.MeetingStartTime != null)
            {
                currentCourse.MeetingStartTime = Course.MeetingStartTime;
            }
            else
            {
                currentCourse.MeetingStartTime = currentCourse.MeetingStartTime;
            }

            if (Course.MeetingEndTime != null)
            {
                currentCourse.MeetingEndTime = Course.MeetingEndTime;
            }
            else
            {
                currentCourse.MeetingEndTime = currentCourse.MeetingEndTime;
            }

            try
            {
                _context.Course.Update(currentCourse);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = id });
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseID == id);
        }
    }
}
