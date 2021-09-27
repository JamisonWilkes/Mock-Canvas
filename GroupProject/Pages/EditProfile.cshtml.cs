using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;
using System.IO;

namespace WebApplicationHW1.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context context;
        private IWebHostEnvironment Environment;

        public EditProfileModel(WebApplicationHW1.Data.WebApplicationHW1Context _context, IWebHostEnvironment _environment)
        {
            context = _context;
            Environment = _environment;
        }
        [BindProperty]
        public UserInfo UserInfo { get; set; }

        [BindProperty]
        public FileTable FileTable { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(List<IFormFile> postedFiles)
        {

            UserInfo CurrentAccount = context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);

                    CurrentAccount.ImageName = fileName;

                    //To get the image to show, I changed the path to read from the Session data
                    //CurrentAccount.ProfileImageFilePath = ("~\\Uploads\\" + fileName);
                    CurrentAccount.ProfileImageFilePath = ("/Uploads/" + fileName);
                    HttpContext.Session.SetString("ProfileImageFilePath", CurrentAccount.ProfileImageFilePath);
                }
            }

            if (UserInfo.FirstName != null)
            {
                CurrentAccount.FirstName = UserInfo.FirstName;
                HttpContext.Session.SetString("FirstName", CurrentAccount.FirstName);

            }
            else
            {
                CurrentAccount.FirstName = CurrentAccount.FirstName;
                HttpContext.Session.SetString("FirstName", CurrentAccount.FirstName);

            }

            if (UserInfo.LastName != null)
            {
                CurrentAccount.LastName = UserInfo.LastName;
                HttpContext.Session.SetString("LastName", CurrentAccount.LastName);

            }
            else
            {
                CurrentAccount.LastName = CurrentAccount.LastName;
                HttpContext.Session.SetString("LastName", CurrentAccount.LastName);

            }

            if (UserInfo.BirthDate != null)
            {
                CurrentAccount.BirthDate = UserInfo.BirthDate;
                HttpContext.Session.SetString("BirthDate", CurrentAccount.BirthDate.ToString());

            }
            else
            {
                CurrentAccount.BirthDate = CurrentAccount.BirthDate;
                HttpContext.Session.SetString("BirthDate", CurrentAccount.BirthDate.ToString());

            }

            if (UserInfo.PhoneNumber != null)
            {
                CurrentAccount.PhoneNumber = UserInfo.PhoneNumber;
                HttpContext.Session.SetString("PhoneNumber", CurrentAccount.PhoneNumber);

            }
            else
            {
                CurrentAccount.PhoneNumber = CurrentAccount.PhoneNumber;
                HttpContext.Session.SetString("PhoneNumber", CurrentAccount.PhoneNumber);

            }

            if (UserInfo.StreetAddress != null)
            {
                CurrentAccount.StreetAddress = UserInfo.StreetAddress;
                HttpContext.Session.SetString("StreetAddress", CurrentAccount.StreetAddress);

            }
            else
            {
                CurrentAccount.StreetAddress = CurrentAccount.StreetAddress;
                HttpContext.Session.SetString("StreetAddress", CurrentAccount.StreetAddress);

            }

            if (UserInfo.City != null)
            {
                CurrentAccount.City = UserInfo.City;
                HttpContext.Session.SetString("City", CurrentAccount.City);

            }
            else
            {
                CurrentAccount.City = CurrentAccount.City;
                HttpContext.Session.SetString("City", CurrentAccount.City);

            }

            if (UserInfo.State != null)
            {
                CurrentAccount.State = UserInfo.State;
                HttpContext.Session.SetString("State", CurrentAccount.State);

            }
            else
            {
                CurrentAccount.State = CurrentAccount.State;
                HttpContext.Session.SetString("State", CurrentAccount.State);

            }

            if (UserInfo.Zip != null)
            {
                CurrentAccount.Zip = UserInfo.Zip;
                HttpContext.Session.SetString("Zip", CurrentAccount.Zip);

            }
            else
            {
                CurrentAccount.Zip = CurrentAccount.Zip;
                HttpContext.Session.SetString("Zip", CurrentAccount.Zip);

            }

            if (UserInfo.Link1 != null)
            {
                CurrentAccount.Link1 = UserInfo.Link1;
                HttpContext.Session.SetString("Link1", CurrentAccount.Link1.ToString());

            }
            else
            {
                CurrentAccount.Link1 = CurrentAccount.Link1;
                HttpContext.Session.SetString("Link1", CurrentAccount.Link1.ToString());

            }

            if (UserInfo.Link2 != null)
            {
                CurrentAccount.Link2 = UserInfo.Link2;
                HttpContext.Session.SetString("Link2", CurrentAccount.Link2.ToString());

            }
            else
            {
                CurrentAccount.Link2 = CurrentAccount.Link2;
                HttpContext.Session.SetString("Link2", CurrentAccount.Link2.ToString());

            }

            if (UserInfo.Link3 != null)
            {
                CurrentAccount.Link3 = UserInfo.Link3;
                HttpContext.Session.SetString("Link3", CurrentAccount.Link3.ToString());

            }
            else
            {
                CurrentAccount.Link3 = CurrentAccount.Link3;
                HttpContext.Session.SetString("Link3", CurrentAccount.Link3.ToString());

            }





            context.UserInfo.Update(CurrentAccount);
            context.SaveChanges();

            return RedirectToPage("/Profile", UserInfo.FirstName, UserInfo.LastName);


        }


    }
}
