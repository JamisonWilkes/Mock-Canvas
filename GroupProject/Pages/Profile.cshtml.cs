using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;


namespace WebApplicationHW1.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;

        public ProfileModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context; 
        }
        [BindProperty]
        public UserInfo UserInfo { get; set; }


        private WebApplicationHW1Context db;

        public void SignUpModel(WebApplicationHW1Context _db)
        {
            db = _db;
        }
        public void OnGet()
        {
        }

        
    }
}
