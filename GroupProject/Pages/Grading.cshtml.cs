using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages
{
    public class GradingModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;
        //private IWebHostEnvironment Environment;
        private IHostingEnvironment Environment;

        [BindProperty]
        public UserInfo UserInfo { get; set; }
        //public Assignments Assignments { get; set; }
        public Course Course { get; set; }

        public int newGrade;

        public AssignmentSubmissions AssignmentSubmissions { get; set; }
        public AssignmentSubmissions currentAssignmentSubmission;
        //public AssignmentSubmissions singleSubmission;

        

        public GradingModel(WebApplicationHW1.Data.WebApplicationHW1Context context, IHostingEnvironment _environment)
        {
            _context = context;
            this.Environment = _environment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            string AccountType = HttpContext.Session.GetString("AccountType");


            AssignmentSubmissions = await _context.AssignmentSubmissions
                .Include(x => x.Assignments)
                .FirstOrDefaultAsync(a => a.SubmissionID == id);

            //currentAssignmentSubmission = new AssignmentSubmissions();
            //currentAssignmentSubmission = AssignmentSubmissions;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            AssignmentSubmissions = await _context.AssignmentSubmissions
                .Include(x => x.Assignments)
                .FirstOrDefaultAsync(a => a.SubmissionID == id);

            currentAssignmentSubmission = new AssignmentSubmissions();
            currentAssignmentSubmission = AssignmentSubmissions;
            currentAssignmentSubmission.Grade = Convert.ToInt32(Request.Form["assignmentGrade"]);
            currentAssignmentSubmission.textEntrySubmission = Request.Form["textEntry"];
            _context.AssignmentSubmissions.Update(currentAssignmentSubmission);
            _context.SaveChanges();
            return RedirectToPage("./Courses/Assignment/Details", new { id = currentAssignmentSubmission.AssignmentID });
        }



    }
}
