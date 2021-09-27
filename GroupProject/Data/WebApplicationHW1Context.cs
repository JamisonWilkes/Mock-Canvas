using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationHW1.Models;

namespace WebApplicationHW1.Data
{
    public class WebApplicationHW1Context : DbContext
    {
        public WebApplicationHW1Context (DbContextOptions<WebApplicationHW1Context> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<WebApplicationHW1.Models.Course> Course { get; set; }

        public DbSet<Payments> Payments { get; set; }

        public DbSet<StudentRegistration> CourseRegistrations { get; set; }

        public DbSet<WebApplicationHW1.Models.Assignments> Assignments { get; set; }

        public DbSet<WebApplicationHW1.Models.AssignmentSubmissions> AssignmentSubmissions { get; set; }


    }
}
