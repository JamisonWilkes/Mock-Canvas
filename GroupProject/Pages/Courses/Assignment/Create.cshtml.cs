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

namespace WebApplicationHW1.Pages.Courses.Assignment
{
    public class CreateModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public CreateModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }

        public IActionResult OnGet()
        {
            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));

            //ViewData["CourseID"] = new SelectList(_context.Course.Where(u => u.UserInfoID==CurrentAccount.ID), "CourseID", "CourseTitle");
            //ViewData["UserInfoID"] = new SelectList(_context.UserInfo, "ID", "LastName");
            return Page();
        }

        [BindProperty]
        public Assignments Assignments { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            if (id == null)
            {
                return NotFound();
            }
            // make an asynchronous call to createAssignment method. 
            // Look at the return value. 
            // if false , return NotFound()
            // if true, use line 55
            bool returnValue = await createAssignmentAsync(CurrentAccount.ID, Request.Form["FileType"], id.Value);
            //createAssignmentAsync(CurrentAccount.ID, Request.Form["FileType"], id.Value );
            if (!returnValue)
                return NotFound();
            else
                return RedirectToPage("/Courses/CourseHomePage", new { id = Assignments.CourseID });

            //return RedirectToPage("/Courses/CourseHomePage", new { id = Assignments.CourseID });
        }


        // make this method asynchronous and make it return boolean using async and Task keyword. 
        // If successful, return true or return false. 
        public async Task<bool> createAssignmentAsync(int userId, string fileType, int courseId)
        {
            Assignments.UserInfoID = userId;


            Course = await _context.Course.FirstOrDefaultAsync(m => m.CourseID == courseId);

            Assignments.CourseID = (int)courseId;

            if (Course == null)
            {
                return false;
            }

            Assignments.FileType = fileType;


            _context.Assignments.Add(Assignments);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
