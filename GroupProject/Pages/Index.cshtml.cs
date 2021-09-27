using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;


namespace WebApplicationHW1.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public UserInfo LogIn { get; set; }

        private readonly WebApplicationHW1.Data.WebApplicationHW1Context context;

        public string msg;

        public List<Course> currentCourses = new List<Course>();

        public List<Course> InstructorCourses = new List<Course>();

        public List<StudentRegistration> currentRegistrations { get; set; }

        public List<Course> Courses { get; set; }

        public IndexModel(WebApplicationHW1.Data.WebApplicationHW1Context _context)
        {
            context = _context;
        }

        public void OnGet()
        {
            LogIn = new UserInfo();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }
        /// <summary>
        /// Method operates after the submittion of the form/Verifies login
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            //Variabe holds the account after being verified with login method
            var acc = login(LogIn.EmailAddress, LogIn.Password);
            //If account is null
            if (acc == null)
            {
                msg = "Invalid";
                return Page();
            }
            else
            {
                //when a student logs in, query for active courses and add
                //them to a List<Course> and save them in the Session
                if (acc.AccountType.ToString() == "Student")
                {
                    currentRegistrations = context.CourseRegistrations
                        .Include(c => c.Course)
                        .ThenInclude(c => c.UserInfo)
                        .Where(c => c.UserInfoID == acc.ID).ToList();

                    foreach (var item in currentRegistrations)
                    {
                        currentCourses.Add(item.Course);
                    }

                    HttpContext.Session.Add<List<Course>>("CurrentCourses", currentCourses);
                }

                //when an instructor logs in, query for active courses and add
                //them to a List<Course> and save them in the Session
                if (acc.AccountType.ToString() == "Instructor")
                {
                    Courses = context.Course
                       .Where(u => u.UserInfoID == acc.ID)
                       .ToList();

                    foreach(var item in Courses)
                    {
                        InstructorCourses.Add(item);
                    }

                    HttpContext.Session.Add<List<Course>>("InstructorCourses", InstructorCourses);
                }

                //All of this session data is required and already in the database by default

                HttpContext.Session.SetString("UserId", acc.ID.ToString());
                HttpContext.Session.SetString("EmailAddress", acc.EmailAddress);
                HttpContext.Session.SetString("Password", acc.Password);
                HttpContext.Session.SetString("FirstName", acc.FirstName);
                HttpContext.Session.SetString("LastName", acc.LastName);
                HttpContext.Session.SetString("BirthDate", acc.BirthDate.ToString());
                HttpContext.Session.SetString("AccountType", acc.AccountType.ToString());
                HttpContext.Session.SetString("ConfirmPassword", acc.ConfirmPassword);



                //session variable to hold the amount of courses the student is enrolled in
                HttpContext.Session.SetInt32("CreditHours", 0);

                //All of this data is optional, so if there is not the data in the UserInfo table, then the session data is defaulted to an empty string
                if (acc.StripeID == null)
                {
                    HttpContext.Session.SetString("StripeID", " ");
                }
                else
                {
                    HttpContext.Session.SetString("StripeID", acc.StripeID);
                }

                if (acc.Tuition == null)
                {
                    HttpContext.Session.SetString("Tuition", " ");
                }
                else
                {
                    HttpContext.Session.SetString("Tuition", acc.Tuition.ToString());
                }

                if (acc.ProfileImageFilePath == null)
                {
                    HttpContext.Session.SetString("ProfileImageFilePath", " ");
                }
                else
                {
                    HttpContext.Session.SetString("ProfileImageFilePath", acc.ProfileImageFilePath);
                }

                if (acc.AccountType.ToString() == "Student")
                {
                    HttpContext.Session.SetString("AccountType", "Student");
                }
                else if (acc.AccountType.ToString() == "Instructor")
                {
                    HttpContext.Session.SetString("AccountType", "Instructor");
                }

                if (acc.PhoneNumber == null)
                {
                    HttpContext.Session.SetString("PhoneNumber", " ");
                }
                else
                {
                    HttpContext.Session.SetString("PhoneNumber", acc.PhoneNumber);
                }

                if (acc.StreetAddress == null)
                {
                    HttpContext.Session.SetString("StreetAddress", " ");
                }
                else
                {
                    HttpContext.Session.SetString("StreetAddress", acc.StreetAddress);
                }

                if (acc.City == null)
                {
                    HttpContext.Session.SetString("City", " ");
                }
                else
                {
                    HttpContext.Session.SetString("City", acc.City);
                }

                if (acc.State == null)
                {
                    HttpContext.Session.SetString("State", " ");
                }
                else
                {
                    HttpContext.Session.SetString("State", acc.State);
                }

                if (acc.Zip == null)
                {
                    HttpContext.Session.SetString("Zip", " ");
                }
                else
                {
                    HttpContext.Session.SetString("Zip", acc.Zip);
                }

                if (acc.Link1 == null)
                {
                    HttpContext.Session.SetString("Link1", " ");
                }
                else
                {
                    HttpContext.Session.SetString("Link1", acc.Link1);
                }

                if (acc.Link2 == null)
                {
                    HttpContext.Session.SetString("Link2", " ");
                }
                else
                {
                    HttpContext.Session.SetString("Link2", acc.Link2);
                }

                if (acc.Link3 == null)
                {
                    HttpContext.Session.SetString("Link3", " ");
                }
                else
                {
                    HttpContext.Session.SetString("Link3", acc.Link3);
                }


                if (acc.AccountType.ToString() == "Instructor")
                {
                    return RedirectToPage("/Courses/Index", new { id = acc.ID });

                }
                else
                {
                    return RedirectToPage("/Registration/RegisteredClasses", new { id = acc.ID });
                }

            }

        }
        /// <summary>
        /// Method checks login process for email address and password
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private UserInfo login(string emailAddress, string password)
        {
            //Account variable equal to the logged in userInfo object
            var account = context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(emailAddress));
            //If account is not null
            if (account != null)
            {
                //If the user inputted password matches the hash stored in account.password return the account
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }


    }
    //class that extends Session methods to include List storage and retrieval
    public static class SessionCoreExtensions
    {
        public static void Add<T>(this ISession iSession, string key, T data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            iSession.SetString(key, serializedData);
        }
        public static List<T> Get<T>(this ISession iSession, string key)
        {
            var data = iSession.GetString(key);
            if (null != data)
                return JsonConvert.DeserializeObject<List<T>>(data);
            return default(List<T>);
        }
    }
}
