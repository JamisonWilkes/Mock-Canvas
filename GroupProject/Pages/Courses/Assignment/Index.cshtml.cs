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
    public class IndexModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public IndexModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public IList<Assignments> Assignments { get;set; }

        public async Task OnGetAsync()
        {
            Assignments = await _context.Assignments
                .Include(a => a.Course)
                .Include(a => a.UserInfo).ToListAsync();
        }
    }
}
