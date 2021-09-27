using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WebApplicationHW1.Data;
using WebApplicationHW1.Models;
using Xunit.Sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApplicationHW1.Pages;

namespace emptyTest
{
    [TestClass]
    public class ProjectUnitTests
    {
        public WebApplicationHW1Context _context;

        [TestMethod]
        public void InstructorCreatesCourseTest()
        {
            //strt with existing instructor that has created n courses
            //use our code to make the instructor create 1 extra course
            //check that instructor is now teaching n + 1 courses
           
            int userId = 1104;

            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);
            //replace number 1 with a known instructor's UserInfoID. 
            var N = _context.Course.Where(x => x.UserInfoID == userId).ToList().Count;
            // continue with the test.........................

            WebApplicationHW1.Pages.Courses.CreateModel instructorCourse = new WebApplicationHW1.Pages.Courses.CreateModel(_context);
            Course course = new Course();
            course.CourseTitle = "Software Engineering";
            course.CourseDescription = "description";
            course.BuildingName = "D1";
            course.CourseNumber = "22";
            course.RoomNumber = "1";
            instructorCourse.Course = course;

            instructorCourse.createCourse(userId, "Math", "test1", "test2", "test3", "Andre", "Marshall");

            var Nn = _context.Course.Where(x => x.UserInfoID == userId).ToList().Count;

            Assert.IsTrue(Nn == N + 1);


        }
        
        [TestMethod]
        public async Task InstructorCreateAssignmentTestAsync()
        {
            //Connect to database
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            int userId = 1104;
            int courseId = 1149;

            //Get the count of assignments for an instructor/Replace 10 with any instructor id 
            var N = _context.Assignments.Where(x => x.CourseID == courseId).ToList().Count;

            WebApplicationHW1.Pages.Courses.Assignment.CreateModel instructorAssignment = new WebApplicationHW1.Pages.Courses.Assignment.CreateModel(_context);
            Assignments assignment = new Assignments();
            assignment.AssignmentTitle = "Test100";
            assignment.AssignmentMaxPoints = 100;
            assignment.AssignmentDescription = "Testing Unit Test";
            assignment.AssignmentDueDate = DateTime.Today;
            assignment.CourseID = courseId;
            instructorAssignment.Assignments = assignment;

            bool returnVal = await instructorAssignment.createAssignmentAsync(userId, "Text Entry", courseId);

            var Nn = _context.Assignments.Where(x => x.CourseID == courseId).ToList().Count;

            Assert.IsTrue(Nn == N + 1);

        }


        [TestMethod]
        public void StudentViewAssignments()
        {
            //Preperation or setup

            //Connect to database
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            WebApplicationHW1.Pages.DashboardModel studentDashboard = new WebApplicationHW1.Pages.DashboardModel(_context);
            UserInfo UserInfo = new UserInfo();
            UserInfo = (UserInfo)_context.UserInfo.Include(s =>s.Registrations).ThenInclude(e => e.Course).ThenInclude(a => a.Assignments).FirstOrDefault(s => s.ID == 1107);

            int numAssignments = 0;
            Assignments SingleAssignment;
            List<Assignments> assignmentList = new List<Assignments>();
            List<Assignments> actualAssignmentsList = new List<Assignments>();
            foreach (var item in UserInfo.Registrations)
            {
                foreach(var course in item.Course.Assignments)
                {
                    SingleAssignment = new Assignments();
                    SingleAssignment.AssignmentID = course.AssignmentID;
                    SingleAssignment.AssignmentTitle = course.AssignmentTitle;
                    SingleAssignment.AssignmentDescription = course.AssignmentDescription;
                    SingleAssignment.AssignmentDueDate = course.AssignmentDueDate;
                    SingleAssignment.AssignmentMaxPoints = course.AssignmentMaxPoints;
                    SingleAssignment.CourseID = course.CourseID;
                    SingleAssignment.UserInfoID = course.UserInfoID;
                    SingleAssignment.FileType = course.FileType;
                    SingleAssignment.Course = course.Course;
                    assignmentList.Add(SingleAssignment);
                    numAssignments++;
                }
            }

            //Perform the operation
            studentDashboard.UserInfo = UserInfo;

            //studentDashboard.GetAllAssignments();
            actualAssignmentsList = studentDashboard.AllAssignments;
            //verify and interpret the results

            assignmentList.SequenceEqual(actualAssignmentsList);

            
        }

        [TestMethod]
        public void IsPaymentSuccessfulTest()
        {

            //step 1: preparation or setup
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);
            TuitionModel tuitionModel = new TuitionModel(_context);

            int userId = 1107;

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.ID == userId);

            ProcessPayment processPayment = new ProcessPayment();

            var cardNumber = "4242424242424242";
            var expireMonth = "01";
            var expireYear = "2030";
            double paymentAmount = 10.00;

            //step 2: perform the operation

            //create the payment method
            var payMethod = processPayment.CreatePaymentMethod(cardNumber, expireMonth, expireYear);
            string methodString = payMethod.Result;
            PaymentMethods  serializedData = JsonConvert.DeserializeObject<PaymentMethods>(methodString);
            string paymentMethodID = serializedData.id;

            //value isNotNull if payment method was successfully created
            Assert.IsNotNull(paymentMethodID);

            //create a payment intent
            ProcessPayment intentToPay = new ProcessPayment();
            var intent_value = intentToPay.CreatePaymentIntent(CurrentAccount.EmailAddress, (Convert.ToDouble(paymentAmount) * 100).ToString());
            var intent_string = intent_value.Result;
            PaymentMethods intent_serialized = JsonConvert.DeserializeObject<PaymentMethods>(intent_string);
            string paymentIntentID = intent_serialized.id;

            //value isNotNull if payment intent was successfully created
            Assert.IsNotNull(paymentIntentID);

            //process the payment
            ProcessPayment makePayment= new ProcessPayment();
            var confirm_value = makePayment.ConfirmPayment(paymentMethodID, paymentIntentID);
            var confirm_string = confirm_value.Result;
            PaymentMethods confirm_serialized = JsonConvert.DeserializeObject<PaymentMethods>(confirm_string);
            string resultMessage = confirm_serialized.status.ToString();


            //step 3: verify and interpret the results
            Assert.AreEqual(resultMessage, "succeeded");

        }
        
        [TestMethod]
        public void CreateInstructorTest()
        {
            //start with a new user
            //use our code to make the instructor 1 new user is added and is an instructor
            //check that user is the last user in the table
            //Check that total accounts are now n + 1

            //step 1: preparation or setup
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);
            
            var N = _context.UserInfo.ToList().Count;
            
            //step 2: perform the operation
            WebApplicationHW1.Pages.Users.CreateModel instructor = new WebApplicationHW1.Pages.Users.CreateModel(_context);
            UserInfo user = new UserInfo();
            user.EmailAddress = "test1@gmail.com";
            user.Password = "passwordTest1!";
            user.FirstName = "Sam";
            user.LastName = "Ple";
            user.BirthDate = new DateTime(1990, 12, 31, 5, 10, 20);
            user.AccountType = 0;
            instructor.createUser(user);

            


            //check
            var Nn = _context.UserInfo.ToList().Count;

            Assert.IsTrue(_context.UserInfo.ToList().Last().ID == user.ID && Nn == N+1);


        }

        [TestMethod]
        public void CreateStudentTest()
        {


            //step 1: preparation or setup
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            var N = _context.UserInfo.ToList().Count;

            //step 2: perform the operation
            WebApplicationHW1.Pages.Users.CreateModel student = new WebApplicationHW1.Pages.Users.CreateModel(_context);
            UserInfo user = new UserInfo();
            user.EmailAddress = "test22@gmail.com";
            user.Password = "passwordTest22!";
            user.FirstName = "Hello";
            user.LastName = "World";
            user.BirthDate = new DateTime(1998, 10, 31, 5, 10, 20);
            user.AccountType = (AccountType)1;
            student.createUser(user);




            //check
            var Nn = _context.UserInfo.ToList().Count;

            Assert.IsTrue(_context.UserInfo.ToList().Last().ID == user.ID && Nn == N + 1);
        }
        [TestMethod]
        public void EditProfile()
        {


            //step 1: preparation or setup
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            UserInfo UserInfo = new UserInfo();
            //UserInfo = (UserInfo)_context.UserInfo.Where(x => x.ID == 1070);

            //step 2: perform the operation
            WebApplicationHW1.Pages.Users.CreateModel changedProfile = new WebApplicationHW1.Pages.Users.CreateModel(_context);
            UserInfo.EmailAddress = "test1@gmail.com";
            UserInfo.Password = "passwordTest1!";
            UserInfo.FirstName = "Sam";
            UserInfo.LastName = "Ple";
            UserInfo.BirthDate = new DateTime(1990, 12, 31, 5, 10, 20);
            UserInfo.AccountType = 0;
            changedProfile.createUser(UserInfo);
            UserInfo.StreetAddress = "123 New ST";
            UserInfo.City = "SLC";
            UserInfo.State = "UT";
            UserInfo.Zip = "84014";
            changedProfile.updateUser(UserInfo);




            //check
            Assert.IsTrue("123 New ST" == UserInfo.StreetAddress);
        }

        [TestMethod]
        public async Task InstructorDeleteCourse()
        {
            //Connect to database
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            //Count number of courses taught
            var N = _context.Course.ToList().Count;

            //get last courses created
            var Y = _context.Course.Max(p => p.CourseID);

            //Create details object reference for access to deleteCourse method
            WebApplicationHW1.Pages.Courses.DetailsModel course = new WebApplicationHW1.Pages.Courses.DetailsModel(_context);

            //Put the courseId in the method call of the course you wish to delete
            bool returnValue = await course.deleteCourse(Y);
            //Get count after deleting course
            var Nn = _context.Course.ToList().Count;
            //Check values
            Assert.IsTrue(Nn == N - 1);
        }

        [TestMethod]
        public async Task InstructorDeleteAssignment()
        {
            //Connect to database
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);
            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            //Count number of assignments 
            var N = _context.Assignments.ToList().Count;

            //get last assignment created
            var Y = _context.Assignments.Max(p => p.AssignmentID);

            //Create delete object reference for access to deleteAssignment method
            WebApplicationHW1.Pages.Courses.Assignment.DeleteModel assignment = new WebApplicationHW1.Pages.Courses.Assignment.DeleteModel(_context);

            //Put the assignmentID in the method call of the assignment you wish to delete
            bool returnValue = await assignment.DeleteAssignmentAsync(Y);
            //Get count after deleting assignment
            var Nn = _context.Assignments.ToList().Count;
            //Check values
            Assert.IsTrue(Nn == N - 1);
        }

        [TestMethod]
        public async Task EditProfileTest()
        {
            //Step 1: preparation and setup
            //Connect to database
            DbContextOptions<WebApplicationHW1Context> options = new DbContextOptions<WebApplicationHW1Context>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSCSharp;User ID=LMSCSharp;Password=Password*1;;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", null);

            _context = new WebApplicationHW1Context((DbContextOptions<WebApplicationHW1Context>)builder.Options);

            UserInfo CurrentAccount = _context.UserInfo.SingleOrDefault(u => u.EmailAddress.Equals("travis@test.com"));

            //change FirstName
            var currentName = CurrentAccount.FirstName;
            var newName = "Justin";

            var currentAddress = CurrentAccount.StreetAddress;
            var newAddress = "1235 Westerchire Ave";

            //step 2: perform the operation
            CurrentAccount.FirstName = newName;
            CurrentAccount.StreetAddress = newAddress;

            //step 3: verify and interpret the results
            Assert.AreNotEqual(currentName, newName);
            Assert.AreNotEqual(currentAddress, newAddress);

        }

    }
    }
