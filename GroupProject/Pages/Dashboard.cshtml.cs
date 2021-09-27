using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public DashboardModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<StudentRegistration> Registrations { get; set; }
        [BindProperty]
        public IList<Course> RegisteredCourses { get; set; }
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

            if (AccountType == "Student")
            {

                //used to access the current assignments for the student
                //NOT used to access course data for course cards (see Session implementation on Dashboard.cshtml)
               Registrations = await _context.CourseRegistrations
                    .Include(u => u.UserInfo)
                    .Where(u => u.UserInfoID == id)
                    .ToListAsync();

               RegisteredCourses = await _context.Course
                    .Include(c => c.Assignments)
                    .ToListAsync();

                GetAllAssignments();
                GetFutureAssignments();

            }

            Assignments = await _context.Assignments.ToListAsync();
            return Page();
        }


        public List<Assignments> GetAllAssignments()
        {
            DateTime localDate = DateTime.Now;

            //This for loop iterates through all of the Users registrations
            foreach (var item in Registrations)
            {
                //this for loop iterates trhough each course a student is registered for and loops through the courses
                //Assignments and adds that assignement to the local List of AllAssignements

                foreach (var thing in item.Course.Assignments)
                {

                    SingleAssignment = new Assignments();
                    SingleAssignment.AssignmentID = thing.AssignmentID;
                    HttpContext.Session.SetInt32("AssignmentID", thing.AssignmentID);
                    SingleAssignment.AssignmentTitle = thing.AssignmentTitle;
                    SingleAssignment.AssignmentDescription = thing.AssignmentDescription;
                    SingleAssignment.AssignmentDueDate = thing.AssignmentDueDate;
                    SingleAssignment.AssignmentMaxPoints = thing.AssignmentMaxPoints;
                    SingleAssignment.CourseID = thing.CourseID;
                    HttpContext.Session.SetInt32("CourseID", thing.CourseID);
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
            foreach (var item in Registrations)
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
