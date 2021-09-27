using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses.Assignment
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;
        private IWebHostEnvironment Environment;

        public DetailsModel(WebApplicationHW1.Data.WebApplicationHW1Context context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        public Assignments Assignments;
        public Course Course { get; set; }


        [BindProperty]
        public AssignmentSubmissions AssignmentSubmissions { get; set; }

        public IList<AssignmentSubmissions> submissions { get; set; }

        public AssignmentSubmissions singleSubmission;

        public int min;
        public int high;
        public double average;
        public double grade;
        public int[] scores = new int[5];
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            string AccountType = HttpContext.Session.GetString("AccountType");

            Assignments = new Assignments();
            Assignments = await _context.Assignments.FirstOrDefaultAsync(m => m.AssignmentID == id);
            
            if(AccountType == "Student")
            {
                if (id.HasValue)
                {
                    submissions = await _context.AssignmentSubmissions.Where(x => x.UserInfoID == CurrentAccount.ID && x.AssignmentID == id.Value).ToListAsync();
                    if (submissions.Count != 0)
                    {
                        high = (int)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).MaxAsync(x => (int?)x.Grade);
                    min = (int)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).MinAsync(x => (int?)x.Grade);
                    average = (double)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).AverageAsync(x => (double?)x.Grade);
                    
                        for (int i = 0; i < submissions.Count; i++)
                        {
                            grade += submissions[i].Grade;
                        }
                        grade = grade / (submissions.Count);//takes the average of all their grades tied to this assignment (change if calculation is different)
                    }
                }
                else
                {
                    high = 0;
                    average = 0;
                    min = 0;
                    grade = 0;
                }
              }

            if (AccountType == "Instructor")
            {
                if (id.HasValue)
                    submissions = await _context.AssignmentSubmissions.Where(x => x.AssignmentID == id.Value).ToListAsync();


                for (int i = 0; i <= submissions.Count -1; i++)
                {
                    double gradePercentage = (double)submissions[i].Grade / (double)Assignments.AssignmentMaxPoints;
                    if(gradePercentage <= .20)
                    {
                        scores[0]++;

                    } else if(gradePercentage <= .40)
                    {
                        scores[1]++;

                    } else if(gradePercentage <= .60)
                    {
                        scores[2]++;

                    } else if(gradePercentage <= .80)
                    {
                        scores[3]++;
                    }
                    else if(gradePercentage <= 1)
                    {
                        scores[4]++;
                    }

                    
                }

                if (submissions.Count != 0)
                {
                    high = (int)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).MaxAsync(x => (int?)x.Grade);
                    min = (int)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).MinAsync(x => (int?)x.Grade);
                    average = (double)await _context.AssignmentSubmissions.Where(x => x.AssignmentID == Assignments.AssignmentID).AverageAsync(x => (double?)x.Grade);
                }
            }

            if (Assignments == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(List<IFormFile> postedFiles, int? id)
        {
            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));

            AssignmentSubmissions.UserInfoID = CurrentAccount.ID;

            AssignmentSubmissions.CourseID = (int)HttpContext.Session.GetInt32("CourseID");

            Assignments = new Assignments();
            Assignments = _context.Assignments.FirstOrDefault(m => m.AssignmentID == id);
            AssignmentSubmissions.Assignments = Assignments;

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Assignments");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = AssignmentSubmissions.UserInfoID + Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);

                    AssignmentSubmissions.SubmissionName = Path.GetFileName(postedFile.FileName);

                    AssignmentSubmissions.SubmissionFilePath = ("/Assignments/" + fileName);

                    HttpContext.Session.SetString("SubmissionFilePath", AssignmentSubmissions.SubmissionFilePath);
                    HttpContext.Session.SetString("SubmissionName", AssignmentSubmissions.SubmissionName);
                }
            }

            if (AssignmentSubmissions.SubmissionFilePath != null)
            {
                AssignmentSubmissions.SubmissionExists = true;
            }

            AssignmentSubmissions.TimeOfSubmission = DateTime.Now;
            HttpContext.Session.SetString("TimeOfSubmission", AssignmentSubmissions.TimeOfSubmission.ToString());

            AssignmentSubmissions.FirstName = CurrentAccount.FirstName;

            AssignmentSubmissions.LastName = CurrentAccount.LastName;

            AssignmentSubmissions.AssignmentID = (int)id;

            if(Assignments.FileType == "Text Entry")
            {
                AssignmentSubmissions.SubmissionName = CurrentAccount.FirstName + CurrentAccount.LastName + Assignments.AssignmentTitle;
                AssignmentSubmissions.textEntrySubmission = Request.Form["textEntry"];
            }

            _context.AssignmentSubmissions.Update(AssignmentSubmissions);

            _context.SaveChanges();

            TempData["Referrer"] = "SaveRegister";
            return RedirectToPage(new { id = id });


        }


    }
}
