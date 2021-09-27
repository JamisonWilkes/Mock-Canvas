using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Pages.Users
{
    public class CreateModel : PageModel
    {

        private readonly WebApplicationHW1.Data.WebApplicationHW1Context context;

        public List<Course> currentCourses = new List<Course>();
        public List<Course> InstructorCourses = new List<Course>();

        public CreateModel(WebApplicationHW1.Data.WebApplicationHW1Context _context)
        {
            context = _context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(context.UserInfo.Where(u => u.EmailAddress == UserInfo.EmailAddress).Any())
            {
                
                return Page();
            }
            UserInfo.Password = BCrypt.Net.BCrypt.HashPassword(UserInfo.Password);
            UserInfo.ConfirmPassword = UserInfo.Password;

            UserInfo.PhoneNumber = " ";
            UserInfo.StreetAddress = " ";
            UserInfo.City = " ";
            UserInfo.State = " ";
            UserInfo.Zip = " ";
            UserInfo.Link1 = " ";
            UserInfo.Link2 = " ";
            UserInfo.Link3 = " ";
            UserInfo.ProfileImageFilePath = " ";
            UserInfo.StripeID = " ";
            UserInfo.Tuition = 0;

            context.UserInfo.Add(UserInfo);
            context.SaveChanges();

            HttpContext.Session.SetString("UserId", UserInfo.ID.ToString());
            HttpContext.Session.SetString("EmailAddress", UserInfo.EmailAddress);
            HttpContext.Session.SetString("Password", UserInfo.Password);
            HttpContext.Session.SetString("FirstName", UserInfo.FirstName);
            HttpContext.Session.SetString("LastName", UserInfo.LastName);
            HttpContext.Session.SetString("BirthDate", UserInfo.BirthDate.ToString());
            HttpContext.Session.SetString("ConfirmPassword", UserInfo.ConfirmPassword);
            


            if (UserInfo.StripeID == null)
            {
                HttpContext.Session.SetString("StripeID", " ");
            }
            else
            {
                HttpContext.Session.SetString("StripeID", UserInfo.StripeID);
            }

            if (UserInfo.Tuition == 0)
            {
                HttpContext.Session.SetString("Tuition", "0");
            }
            else
            {
                HttpContext.Session.SetString("Tuition", UserInfo.Tuition.ToString());
            }

            if (UserInfo.ProfileImageFilePath.ToString() == null)
            {
                HttpContext.Session.SetString("ProfileImageFilePath", " ");
            }
            else 
            {
                HttpContext.Session.SetString("ProfileImageFilePath", UserInfo.ProfileImageFilePath );
            }

            if (UserInfo.AccountType.ToString() == "Student")
            {
                HttpContext.Session.SetString("AccountType", "Student");
            }
            else if (UserInfo.AccountType.ToString() == "Instructor")
            {
                HttpContext.Session.SetString("AccountType", "Instructor");
            }

            if (UserInfo.PhoneNumber == null)
            { 
                HttpContext.Session.SetString("PhoneNumber", "");
            }
            else
            {
                HttpContext.Session.SetString("PhoneNumber", UserInfo.PhoneNumber);
            }

            if (UserInfo.StreetAddress == null)
            {
                HttpContext.Session.SetString("StreetAddress", "");
            }
            else
            {
                HttpContext.Session.SetString("StreetAddress", UserInfo.StreetAddress);
            }

            if (UserInfo.City == null)
            {
                HttpContext.Session.SetString("City", "");
            }
            else
            {
                HttpContext.Session.SetString("City", UserInfo.City);
            }

            if (UserInfo.State == null)
            {
                HttpContext.Session.SetString("State", "");
            }
            else
            {
                HttpContext.Session.SetString("State", UserInfo.State);
            }

            if (UserInfo.Zip == null)
            {
                HttpContext.Session.SetString("Zip", "");
            }
            else
            {
                HttpContext.Session.SetString("Zip", UserInfo.Zip);
            }

            if (UserInfo.Link1 == null)
            {
                HttpContext.Session.SetString("Link1", "");
            }
            else
            {
                HttpContext.Session.SetString("Link1", UserInfo.Link1);
            }

            if (UserInfo.Link2 == null)
            {
                HttpContext.Session.SetString("Link2", "");
            }
            else
            {
                HttpContext.Session.SetString("Link2", UserInfo.Link2);
            }

            if (UserInfo.Link3 == null)
            {
                HttpContext.Session.SetString("Link3", "");
            }
            else
            {
                HttpContext.Session.SetString("Link3", UserInfo.Link3);
            }


            if (UserInfo.AccountType.ToString() == "Instructor")
            {
                HttpContext.Session.Add<List<Course>>("InstructorCourses", InstructorCourses);
                return RedirectToPage("/Courses/Index", new { id = UserInfo.ID });

            }
            else
            {
                HttpContext.Session.Add<List<Course>>("InstructorCourses", currentCourses);
                return RedirectToPage("/Registration/RegisteredClasses", new { id = UserInfo.ID });
            }


        }
        
        //public UserInfo user { get; set; }
        public void createUser(UserInfo user)
        {
            //user.EmailAddress = EmailAddress;
            //user.Password = Password;
            //user.FirstName = FirstName;
            //user.LastName = LastName;
            //user.BirthDate = BirthDate;
            //user.AccountType = AccountType;
            user.ConfirmPassword = user.Password;
         
            context.UserInfo.Add(user);
            context.SaveChanges();
        }
        public void updateUser(UserInfo user)
        {

            user.ConfirmPassword = user.Password;

            context.UserInfo.Update(user);
            context.SaveChanges();
        }
    }
}
