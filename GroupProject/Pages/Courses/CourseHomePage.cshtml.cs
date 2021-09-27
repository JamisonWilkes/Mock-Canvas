using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses
{
    public class CourseHomePageModel : PageModel
    {
        [BindProperty]
        public Assignments assignment { get; set; }

        public IList<Assignments> assignments { get; set; }

        public IList<AssignmentSubmissions> assignmentSubmissions { get; set; }

        public IList<StudentRegistration> studentRegistered { get; set; }

        public IList<StudentRegistration> thisStudentRegistered { get; set; }

        public IList<UserInfo> user { get; set; }

        public double totalPossibleCoursePoints;
        public double studentsPoints;
        public double percentage;

        public int numStudentA;
        public int numStudentAMinus;
        public int numStudentBPlus;
        public int numStudentB;
        public int numStudentBMinus;
        public int numStudentCPlus;
        public int numStudentC;
        public int numStudentCMinus;
        public int numStudentDPlus;
        public int numStudentD;
        public int numStudentDMinus;
        public int numStudentE;

        public UserInfo userInfo { get; set; }

        public StudentRegistration studentRegistration { get; set; }

        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public CourseHomePageModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            userInfo = await _context.UserInfo
                .Include(s => s.Assignments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));


            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseID == id);
            //Course = await _context.Courses.FirstOrDefaultAsync(m => m.UserInfoID == id);

            HttpContext.Session.SetInt32("CourseID", (int)id);
            if (id.HasValue)
                assignments = await _context.Assignments.Where(x => x.CourseID == id.Value).ToListAsync();

            assignment = await _context.Assignments.FirstOrDefaultAsync(m => m.CourseID == id);

            studentRegistration = await _context.CourseRegistrations.FirstOrDefaultAsync(m => m.CourseID == id);

            studentRegistered = await _context.CourseRegistrations.Where(x => x.CourseID == id.Value).ToListAsync();

            thisStudentRegistered = await _context.CourseRegistrations.Where(x => x.UserInfoID == CurrentAccount.ID && x.CourseID == id.Value).ToListAsync();


            totalPossibleCoursePoints = 0;
            studentsPoints = 0;
            numStudentA = 0;
            numStudentAMinus = 0;
            numStudentBPlus = 0;
            numStudentB = 0;
            numStudentBMinus = 0;
            numStudentCPlus = 0;
            numStudentC = 0;
            numStudentCMinus = 0;
            numStudentDPlus = 0;
            numStudentD = 0;
            numStudentDMinus = 0;
            numStudentE = 0;

            if (HttpContext.Session.GetString("AccountType") == "Student")
            {
                assignmentSubmissions = await _context.AssignmentSubmissions.Where(x => x.UserInfoID == CurrentAccount.ID && x.CourseID == id.Value).ToListAsync();

                foreach (var item in assignments)
                {
                    //assignment.AssignmentID = item.AssignmentID;
                    totalPossibleCoursePoints = item.AssignmentMaxPoints + totalPossibleCoursePoints;
                    HttpContext.Session.SetInt32("AssignmentID", item.AssignmentID);

                }

                foreach (var thing in assignmentSubmissions)
                {
                    studentsPoints = thing.Grade + studentsPoints;
                }

                percentage = (studentsPoints / totalPossibleCoursePoints) * 100;

                foreach (var thing in thisStudentRegistered)
                {
                    
                    if (percentage >= 94)
                    {
                        thing.LetterGrade = "A";
                    }
                    else if (percentage >= 90 && percentage < 94)
                    {
                        thing.LetterGrade = "A-";
                    }

                    else if (percentage >= 87 && percentage < 90)
                    {
                        thing.LetterGrade = "B+";
                    }
                    else if (percentage >= 84 && percentage < 87)
                    {
                        thing.LetterGrade = "B";
                    }
                    else if (percentage >= 80 && percentage < 84)
                    {
                        thing.LetterGrade = "B-";
                    }
                    else if (percentage >= 77 && percentage < 80)
                    {
                        thing.LetterGrade = "C+";
                    }
                    else if (percentage >= 74 && percentage < 77)
                    {
                        thing.LetterGrade = "C";
                    }
                    else if (percentage >= 70 && percentage < 74)
                    {
                        thing.LetterGrade = "C-";
                    }
                    else if (percentage >= 67 && percentage < 70)
                    {
                        thing.LetterGrade = "D+";
                    }
                    else if (percentage >= 64 && percentage < 67)
                    {
                        thing.LetterGrade = "D";
                    }
                    else if (percentage >= 60 && percentage < 64)
                    {
                        thing.LetterGrade = "D-";
                    }
                    else if (percentage < 60)
                    {
                        thing.LetterGrade = "E";
                    }
                }

                await _context.SaveChangesAsync();


            }

            else if (HttpContext.Session.GetString("AccountType") == "Instructor")
            {

                numStudentA = 0;
                numStudentAMinus = 0;
                numStudentBPlus = 0;
                numStudentB = 0;
                numStudentBMinus = 0;
                numStudentCPlus = 0;
                numStudentC = 0;
                numStudentCMinus = 0;
                numStudentDPlus = 0;
                numStudentD = 0;
                numStudentDMinus = 0;
                numStudentE = 0;
                HttpContext.Session.SetInt32("numStudentA", numStudentA);
                HttpContext.Session.SetInt32("numStudentAMinus", numStudentAMinus);
                HttpContext.Session.SetInt32("numStudentBPlus", numStudentBPlus);
                HttpContext.Session.SetInt32("numStudentB", numStudentB);
                HttpContext.Session.SetInt32("numStudentBMinus", numStudentBMinus);
                HttpContext.Session.SetInt32("numStudentCPlus", numStudentCPlus);
                HttpContext.Session.SetInt32("numStudentC", numStudentC);
                HttpContext.Session.SetInt32("numStudentCMinus", numStudentCMinus);
                HttpContext.Session.SetInt32("numStudentDPlus", numStudentDPlus);
                HttpContext.Session.SetInt32("numStudentD", numStudentD);
                HttpContext.Session.SetInt32("numStudentDMinus", numStudentDMinus);
                HttpContext.Session.SetInt32("numStudentE", numStudentE);

                foreach (var item in assignments)
                {
                    //assignment.AssignmentID = item.AssignmentID;
                    totalPossibleCoursePoints = item.AssignmentMaxPoints + totalPossibleCoursePoints;
                    HttpContext.Session.SetInt32("AssignmentID", item.AssignmentID);
                }

                foreach (var thing in studentRegistered)
                {
                    studentsPoints = 0;

                    assignmentSubmissions = await _context.AssignmentSubmissions.Where(x => x.UserInfoID == thing.UserInfoID && x.CourseID == thing.CourseID).ToListAsync();


                    foreach (var item in assignmentSubmissions)
                    {
                        studentsPoints = item.Grade + studentsPoints;
                    }

                    percentage = (studentsPoints / totalPossibleCoursePoints) * 100;

                    if (percentage >= 94)
                    {
                        thing.LetterGrade = "A";
                    }
                    else if (percentage >= 90 && percentage < 94)
                    {
                        thing.LetterGrade = "A-";
                    }

                    else if (percentage >= 87 && percentage < 90)
                    {
                        thing.LetterGrade = "B+";
                    }
                    else if (percentage >= 84 && percentage < 87)
                    {
                        thing.LetterGrade = "B";
                    }
                    else if (percentage >= 80 && percentage < 84)
                    {
                        thing.LetterGrade = "B-";
                    }
                    else if (percentage >= 77 && percentage < 80)
                    {
                        thing.LetterGrade = "C+";
                    }
                    else if (percentage >= 74 && percentage < 77)
                    {
                        thing.LetterGrade = "C";
                    }
                    else if (percentage >= 70 && percentage < 74)
                    {
                        thing.LetterGrade = "C-";
                    }
                    else if (percentage >= 67 && percentage < 70)
                    {
                        thing.LetterGrade = "D+";
                    }
                    else if (percentage >= 64 && percentage < 67)
                    {
                        thing.LetterGrade = "D";
                    }
                    else if (percentage >= 60 && percentage < 64)
                    {
                        thing.LetterGrade = "D-";
                    }
                    else if (percentage < 60)
                    {
                        thing.LetterGrade = "E";
                    }

                    await _context.SaveChangesAsync();
                }

                int totalNumStudentsInCourse = studentRegistered.Count;
                HttpContext.Session.SetInt32("totalNumStudentsInCourse", totalNumStudentsInCourse);

                foreach (var thing in studentRegistered)
                {
                    if(thing.LetterGrade != null)
                    {
                        if (thing.LetterGrade.Equals("A"))
                        {
                            numStudentA++;
                            HttpContext.Session.SetInt32("numStudentA", numStudentA);
                        }
                        else if (thing.LetterGrade.Equals("A-"))
                        {
                            numStudentAMinus++;
                            HttpContext.Session.SetInt32("numStudentAMinus", numStudentAMinus);
                        }
                        else if (thing.LetterGrade.Equals("B+"))
                        {
                            numStudentBPlus++;
                            HttpContext.Session.SetInt32("numStudentBPlus", numStudentBPlus);
                        }
                        else if (thing.LetterGrade.Equals("B"))
                        {
                            numStudentB++;
                            HttpContext.Session.SetInt32("numStudentB", numStudentB);
                        }
                        else if (thing.LetterGrade.Equals("B-"))
                        {
                            numStudentBMinus++;
                            HttpContext.Session.SetInt32("numStudentBMinus", numStudentBMinus);
                        }
                        else if (thing.LetterGrade.Equals("C+"))
                        {
                            numStudentCPlus++;
                            HttpContext.Session.SetInt32("numStudentCPlus", numStudentCPlus);
                        }
                        else if (thing.LetterGrade.Equals("C"))
                        {
                            numStudentC++;
                            HttpContext.Session.SetInt32("numStudentC", numStudentC);
                        }
                        else if (thing.LetterGrade.Equals("C-"))
                        {
                            numStudentCMinus++;
                            HttpContext.Session.SetInt32("numStudentCMinus", numStudentCMinus);
                        }
                        else if (thing.LetterGrade.Equals("D+"))
                        {
                            numStudentDPlus++;
                            HttpContext.Session.SetInt32("numStudentDPlus", numStudentDPlus);
                        }
                        else if (thing.LetterGrade.Equals("D"))
                        {
                            numStudentD++;
                            HttpContext.Session.SetInt32("numStudentD", numStudentD);
                        }
                        else if (thing.LetterGrade.Equals("D-"))
                        {
                            numStudentDMinus++;
                            HttpContext.Session.SetInt32("numStudentDMinus", numStudentDMinus);
                        }
                        else if (thing.LetterGrade.Equals("E"))
                        {
                            numStudentE++;
                            HttpContext.Session.SetInt32("numStudentE", numStudentE);
                        }
                    }

                }
            }


            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }


    }
}
