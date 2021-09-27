using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses.Assignment
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public DeleteModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignments Assignments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assignments = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.UserInfo).FirstOrDefaultAsync(m => m.AssignmentID == id);

            if (Assignments == null)
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

            bool returnValue = await DeleteAssignmentAsync(id);
            
            if (!returnValue)
                return NotFound();
            else
                return RedirectToPage("/Courses/CourseHomePage", new { id = Assignments.CourseID });

            //return RedirectToPage("/Courses/CourseHomePage", new { id = Assignments.CourseID });
        }

        public async Task<bool> DeleteAssignmentAsync(int? id)
        {
            Assignments = await _context.Assignments.FindAsync(id);

            if (Assignments != null)
            {
                _context.Assignments.Remove(Assignments);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
