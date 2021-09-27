using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Forms
{
    public class SignedInModel : PageModel
    {
        public UserInfo LoggedIn { get; set; }

        public string firstName;
        public string lastName;

        public void OnGet()
        {

        }
    }
}
