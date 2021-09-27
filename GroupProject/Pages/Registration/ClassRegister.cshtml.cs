using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Registration
{
    public class IndexModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public IndexModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentRegistration RegisteredCourse { get; set; }

        public Course Courses { get; set; }

        public UserInfo UserInfo { get; set; }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync(int? id)
        {

            UserInfo = await _context.UserInfo
            .Include(s => s.Registrations)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            //RegisteredCourse = await _context.CourseRegistrations
            //    .Include(c => c.UserInfo)
            //    .FirstOrDefault(m => m.UserInfoID == id);
            
            Course = await _context.Course
                .Include(c => c.UserInfo).ToListAsync();
        }
    }
}
