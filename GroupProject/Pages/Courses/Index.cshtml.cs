using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public IndexModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public UserInfo UserInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            /*UserInfo = await _context.UserInfo
                .Include(s => s.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if(UserInfo == null)
            {
                return NotFound();
            }*/

            return Page();
        }
    }
}
