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

namespace WebApplicationHW1.Pages.Registration
{
    public class CreateModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public CreateModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; }

        public UserInfo UserInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            string AccountType = HttpContext.Session.GetString("AccountType");

            if (id == null)
            {
                return NotFound();
            }

           


            //if (AccountType == "Instructor")
            //{
            //    UserInfo = await _context.UserInfo
            //        .Include(s => s.Courses)
            //        .AsNoTracking()
            //        .FirstOrDefaultAsync(m => m.ID == id);
            //}

            /*if (AccountType == "Student")
            {
                UserInfo = await _context.UserInfo
                    .Include(s => s.Registrations)
                    .ThenInclude(e => e.Course)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);
            }*/
            //UserInfo.RegisteredCreditHours = (int)HttpContext.Session.GetInt32("CreditHours");

            //_context.SaveChanges();

            return Page();
        }
    }
}
