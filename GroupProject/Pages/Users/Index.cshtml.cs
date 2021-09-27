using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public IndexModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        public IList<UserInfo> UserInfo { get;set; }
        [BindProperty(SupportsGet = true)]
        public string EmailAddress { get; set; }


        public async Task OnGetAsync()
        {
            UserInfo = await _context.UserInfo.ToListAsync();
        }
    }
}
