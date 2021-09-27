using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages
{

    public class CalendarModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public CalendarModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Course> Courses { get; set; }

        public IList<Assignments> Assignments { get; set; }

        public List<Assignments> AllAssignments = new List<Assignments>();

        public List<Assignments> OrderedFutureAssignments = new List<Assignments>();

        public UserInfo UserInfo { get; set; }

        public Assignments SingleAssignment { get; set; }

        public ArrayList registeredCourseIDNumbers = new ArrayList();

        public static DateTime Now { get; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string AccountType = HttpContext.Session.GetString("AccountType");

            if (id == null)
            {
                return NotFound();
            }


            if (AccountType == "Instructor")
            {
                UserInfo = await _context.UserInfo
                    .Include(s => s.Courses)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);
            }

            if (AccountType == "Student")
            {
                UserInfo = await _context.UserInfo
                    .Include(s => s.Registrations)
                    .ThenInclude(e => e.Course)
                    .ThenInclude(a => a.Assignments)
                    .Include(a => a.Assignments)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);

                GetAllAssignments();
                GetFutureAssignments();

            }
            //Courses = await _context.Course.FirstOrDefaultAsync(m => m.CourseID == id);
            Assignments = await _context.Assignments.Where(x => x.CourseID == id.Value).ToListAsync();
            if (UserInfo == null)
            {
                return NotFound();
            }

            return Page();
        }
        public List<Assignments> GetAllAssignments()
        {
            DateTime localDate = DateTime.Now;

            //This for loop iterates through all of the Users registrations
            foreach (var item in UserInfo.Registrations)
            {
                //this for loop iterates trhough each course a student is registered for and loops through the courses
                //Assignments and adds that assignement to the local List of AllAssignements

                foreach (var thing in item.Course.Assignments)
                {

                    SingleAssignment = new Assignments();
                    SingleAssignment.AssignmentID = thing.AssignmentID;
                    SingleAssignment.AssignmentTitle = thing.AssignmentTitle;
                    SingleAssignment.AssignmentDescription = thing.AssignmentDescription;
                    SingleAssignment.AssignmentDueDate = thing.AssignmentDueDate;
                    SingleAssignment.AssignmentMaxPoints = thing.AssignmentMaxPoints;
                    SingleAssignment.CourseID = thing.CourseID;
                    SingleAssignment.UserInfoID = thing.UserInfoID;
                    SingleAssignment.FileType = thing.FileType;
                    SingleAssignment.Course = thing.Course;
                    AllAssignments.Add(SingleAssignment);

                }
            }

            return AllAssignments;
        }

        public List<Assignments> GetFutureAssignments()
        {
            DateTime localDate = DateTime.Now;

            //This for loop iterates through all of the Users registrations
            foreach (var item in UserInfo.Registrations)
            {
                //this for loop iterates trhough each course a student is registered for and loops through the courses
                //Assignments and adds that assignement to the local List of AllAssignements

                foreach (var thing in item.Course.Assignments)
                {

                    if (thing.AssignmentDueDate > localDate)
                    {
                        SingleAssignment = new Assignments();
                        SingleAssignment.AssignmentID = thing.AssignmentID;
                        SingleAssignment.AssignmentTitle = thing.AssignmentTitle;
                        SingleAssignment.AssignmentDescription = thing.AssignmentDescription;
                        SingleAssignment.AssignmentDueDate = thing.AssignmentDueDate;
                        SingleAssignment.AssignmentMaxPoints = thing.AssignmentMaxPoints;
                        SingleAssignment.CourseID = thing.CourseID;
                        SingleAssignment.UserInfoID = thing.UserInfoID;
                        SingleAssignment.FileType = thing.FileType;
                        SingleAssignment.Course = thing.Course;
                        OrderedFutureAssignments.Add(SingleAssignment);
                    }
                }
            }
            return OrderedFutureAssignments;
        }
    }


}
