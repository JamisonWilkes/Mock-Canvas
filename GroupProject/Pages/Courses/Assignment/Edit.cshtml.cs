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
    public class EditModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public EditModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
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
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseTitle");
            ViewData["UserInfoID"] = new SelectList(_context.UserInfo, "ID", "LastName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        { 
            Assignments currentAssignments = _context.Assignments.SingleOrDefault(m => m.AssignmentID == id.Value);

/*            if (!ModelState.IsValid)
            {
                return Page();
            }*/

            /*_context.Attach(Assignments).State = EntityState.Modified;*/

            if(Assignments.AssignmentTitle != null)
            {
                currentAssignments.AssignmentTitle = Assignments.AssignmentTitle;
            }
            else
            {
                currentAssignments.AssignmentTitle = currentAssignments.AssignmentTitle;
            }

            if (Assignments.AssignmentDescription != null)
            {
                currentAssignments.AssignmentDescription = Assignments.AssignmentDescription;
            }
            else
            {
                currentAssignments.AssignmentDescription = currentAssignments.AssignmentDescription;
            }

            if (Assignments.AssignmentMaxPoints.ToString() != null)
            {
                currentAssignments.AssignmentMaxPoints = Assignments.AssignmentMaxPoints;
            }
            else
            {
                currentAssignments.AssignmentDescription = currentAssignments.AssignmentDescription;
            }

            if (Assignments.AssignmentDescription != null)
            {
                currentAssignments.AssignmentDueDate = Assignments.AssignmentDueDate;
            }
            else
            {
                currentAssignments.AssignmentDueDate = currentAssignments.AssignmentDueDate;
            }


            try
            {
                _context.Assignments.Update(currentAssignments);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentsExists(Assignments.AssignmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Assignments.AssignmentID });
        }

        private bool AssignmentsExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentID == id);
        }
    }
}