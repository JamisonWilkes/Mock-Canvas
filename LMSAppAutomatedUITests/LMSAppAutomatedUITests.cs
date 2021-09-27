using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Xunit;

namespace LMSAppAutomatedUITests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        private WebDriverWait wait;

        public IList<IWebElement> controlList1 { get; set; }
        public IList<IWebElement> controlList2 { get; set; }
        public IList<IWebElement> controlList3 { get; set; }
        public IList<IWebElement> controlList4 { get; set; }
        public AutomatedUITests()
        {
            _driver = new ChromeDriver(@"C:\Users\jamis\OneDrive\Desktop\GroupProject\LMSAppAutomatedUITests\chromedrivers\chromedriver91\");
            //_driver = new ChromeDriver(@"C:\Users\AubreyDavidson\Documents\CS 3750\gp\LMSAppAutomatedUITests\chromedrivers\chromedriver91\");
            //_driver = new ChromeDriver(@"C:\Users\03cjm\Desktop\CS3750NewRepo\CS_Assignment1.zip\LMSAppAutomatedUITests\chromedrivers\chromedriver91\");
            _driver.Manage().Window.Maximize();

        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        //the login test
        [Fact]
        public void UserLogin()
        {
            string username = "jamison@gmail.com";
            string password = "jamison101";

            _driver.Navigate().GoToUrl("https://csharpteam.azurewebsites.net/");
            _driver.FindElement(By.Id("username")).SendKeys(username);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("password")).SendKeys(password);
            System.Threading.Thread.Sleep(1000);

            // Click login
            _driver.FindElement(By.Id("loginButton")).Click();
            System.Threading.Thread.Sleep(1000);

            string expectedURL = "https://csharpteam.azurewebsites.net/Courses?id=1104";

            Assert.Equal(expectedURL, _driver.Url);
            _driver.FindElement(By.Id("logoutButton")).Click();
        }

        [Fact]
        public void incorrectUserLogin()
        {
            string username = "notausername@gmail.com";
            string password = "notapassword";
            
            _driver.Navigate().GoToUrl("https://csharpteam.azurewebsites.net/");
            _driver.FindElement(By.Id("username")).SendKeys(username);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("password")).SendKeys(password);
            System.Threading.Thread.Sleep(1000);

            // Click login
            _driver.FindElement(By.Id("loginButton")).Click();
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("username"));

        }
        //Create Course test
        [Fact]
        public void CreateCourse()
        {
            string username = "jamison@gmail.com";
            string password = "jamison101";
            string Department = "Computer Science";
            string MeetingDayOne = "Monday";
            string MeetingDayTwo = "Tuesday";
            string MeetingDayThree = "Wednesday";
            string CourseTitle = "Testing UI";
            string courseNumber = "100";
            string credits = "4";
            string BuildingName = "Testing";
            string RoomNumber = "101";
            DateTime MeetingStartTime = DateTime.Now;
            DateTime MeetingEndTime = DateTime.Now;
            string Description = "this is a test";


            _driver.Navigate().GoToUrl("https://csharpteam.azurewebsites.net/");
            _driver.FindElement(By.Id("username")).SendKeys(username);
            System.Threading.Thread.Sleep(1000);
            _driver.FindElement(By.Id("password")).SendKeys(password);
            System.Threading.Thread.Sleep(1000);
            // Click login
            _driver.FindElement(By.Id("loginButton")).Click();
            System.Threading.Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
           
            
            IWebElement searchButton = _driver.FindElement(By.ClassName("btn"));

            controlList3 = _driver.FindElements(By.XPath(".//*[@class='card text-white m-2 p-0']"));

            //click create
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("department")).SendKeys(Department);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("form-control")).SendKeys(CourseTitle);
            System.Threading.Thread.Sleep(1000);

             controlList1 = _driver.FindElements(By.XPath(".//*[@class='col-2']//input"));

            controlList1[0].SendKeys(courseNumber);
            System.Threading.Thread.Sleep(1000);

            controlList1[1].SendKeys(credits);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("meetingDayOne")).SendKeys(MeetingDayOne);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("meetingDayTwo")).SendKeys(MeetingDayTwo);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("meetingDayThree")).SendKeys(MeetingDayThree);
            System.Threading.Thread.Sleep(1000);

            controlList1[2].SendKeys(BuildingName);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.XPath(".//*[@class='col-1']//input")).SendKeys(RoomNumber);
            System.Threading.Thread.Sleep(1000);

            controlList2 = _driver.FindElements(By.XPath(".//*[@class='col-3']//input"));

            controlList2[0].SendKeys("0245PM");
            System.Threading.Thread.Sleep(1000);

            controlList2[1].SendKeys("0445PM");
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.XPath(".//*[@class='col-md-9']//textarea")).SendKeys(Description);
            System.Threading.Thread.Sleep(1000);

            var Element = _driver.FindElement(By.ClassName("btn"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(700, 700)");
            System.Threading.Thread.Sleep(4000);
            Element.Click();

            System.Threading.Thread.Sleep(4000);

            controlList4 = _driver.FindElements(By.XPath(".//*[@class='card text-white m-2 p-0']"));

            if(controlList4.Count > controlList3.Count)
            {
                controlList4[(controlList4.Count) - 1].Click();
                System.Threading.Thread.Sleep(4000);
            }

        }
        [Fact]
        public void RegisterCourse()
        {
            string username = "student@gmail.com";
            string password = "12345678";
            
            _driver.Navigate().GoToUrl("https://csharpteam.azurewebsites.net/");
            _driver.FindElement(By.Id("username")).SendKeys(username);
            System.Threading.Thread.Sleep(1000);
            _driver.FindElement(By.Id("password")).SendKeys(password);
            System.Threading.Thread.Sleep(1000);
            // Click login
            _driver.FindElement(By.Id("loginButton")).Click();
            System.Threading.Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));


            IWebElement searchButton = _driver.FindElement(By.ClassName("btn"));

            

            //click create
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);
            
            searchButton = _driver.FindElement(By.ClassName("btn-primary"));
            searchButton.Click();

            System.Threading.Thread.Sleep(1000);

            searchButton = _driver.FindElement(By.ClassName("btn-primary"));
            searchButton.Click();

            System.Threading.Thread.Sleep(4);


            
           

        }

        [Fact]
        public void CreateAssignment()
        {
            //Variables needed for test
            string username = "jamison@gmail.com";
            string password = "jamison101";
            string assignmentTitle = "UI test";
            string assignmentDescription = "This is a UI test";
            string pointsPossible = "50";
            string submissionType = "File Submission";

            //Driver naviagates to website and logs in
            _driver.Navigate().GoToUrl("https://csharpteam.azurewebsites.net/");
            //Driver recieves username from UI test variable
            _driver.FindElement(By.Id("username")).SendKeys(username);
            System.Threading.Thread.Sleep(1000);
            _driver.FindElement(By.Id("password")).SendKeys(password);
            System.Threading.Thread.Sleep(1000);
            //Login
            _driver.FindElement(By.Id("loginButton")).Click();
            System.Threading.Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            //Select the dashboard link
            IWebElement searchButton = _driver.FindElement(By.LinkText("Dashboard"));
            //Navigate to dashboard
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);
            //Navigate to selected course
            searchButton = _driver.FindElement(By.ClassName("col-4"));
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);

            //Scroll down to see button 
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, 300)");
            System.Threading.Thread.Sleep(2000);

            //Navigate to create assignment page
            searchButton = _driver.FindElement(By.LinkText("Create Assignment"));
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);
            //Fill in the create assignment values
            _driver.FindElement(By.Id("Assignments_AssignmentTitle")).SendKeys(assignmentTitle);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("Assignments_AssignmentDescription")).SendKeys(assignmentDescription);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("Assignments_AssignmentMaxPoints")).SendKeys(pointsPossible);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("Assignments_AssignmentDueDate")).SendKeys("7302021");
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("Assignments_AssignmentDueDate")).SendKeys(Keys.Tab);
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("Assignments_AssignmentDueDate")).SendKeys("1000AM");
            System.Threading.Thread.Sleep(1000);

            _driver.FindElement(By.Id("filetype")).SendKeys(submissionType);
            System.Threading.Thread.Sleep(1000);
            //Select the create button
            searchButton = _driver.FindElement(By.CssSelector(".btn"));
            searchButton.Click();
            System.Threading.Thread.Sleep(1000);
            //Scroll down to the newly created assignment 
            js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, 300)");
            System.Threading.Thread.Sleep(5000);


        }
    }
}
