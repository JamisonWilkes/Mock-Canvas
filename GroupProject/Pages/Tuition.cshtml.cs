using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using WebApplicationHW1.Models;


namespace WebApplicationHW1.Pages
{
    public class TuitionModel : PageModel
    {
        private readonly WebApplicationHW1.Data.WebApplicationHW1Context _context;
        UserInfo CurrentAccount;

        public TuitionModel(WebApplicationHW1.Data.WebApplicationHW1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Payments Pay { get; set; }

        public UserInfo UserInfo { get; set; }

        public IList<Course> Courses { get; set; }

        public IActionResult OnGet()
        {
            UserInfo = _context.UserInfo
                .SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //set UserInfo object to currently logged in User
            CurrentAccount = _context.UserInfo.
                SingleOrDefault(u => u.EmailAddress.Equals(HttpContext.Session.GetString("EmailAddress")));

            //get the email address of the currently logged in User
            var email = HttpContext.Session.GetString("EmailAddress");

            if (email != null)
            {
                //Create the payment method object using stripe.com api
                //returns the json data of the payment, including the id: pm_...
                ProcessPayment payment = new ProcessPayment();

                //variables to hold the credit card information
                var inputCardNumber = Pay.cardNumber;
                var inputExpiration = Pay.cardExpiration;
                var paymentAmount = Pay.totalPayment;

                //sanitize the input
                var sanitizer = new HtmlSanitizer();

                var cleanCardNumber = sanitizer.Sanitize(inputCardNumber);
                var cleanExpirationMonth = sanitizer.Sanitize(inputExpiration.Month.ToString());
                var cleanExpirationYear = sanitizer.Sanitize(inputExpiration.Year.ToString());
                var cleanPaymentAmount = sanitizer.Sanitize(paymentAmount);

                //Process the Payment Method
                var value = payment.CreatePaymentMethod(cleanCardNumber, cleanExpirationMonth, cleanExpirationYear);

                string valuestring = value.Result;
                PaymentMethods serializedData = JsonConvert.DeserializeObject<PaymentMethods>(valuestring);
                string paymentMethodID = serializedData.id;

                if (paymentMethodID == null)
                {
                    return Redirect("./PaymentFailure");
                }
                else
                {
                    //create the payment intent
                    //returns the json data of the intent, including the id: pi_...
                    ProcessPayment intent = new ProcessPayment();
                    var intent_value = intent.CreatePaymentIntent(email, (Convert.ToDouble(cleanPaymentAmount) * 100).ToString());
                    var intent_string = intent_value.Result;
                    PaymentMethods intent_serialized = JsonConvert.DeserializeObject<PaymentMethods>(intent_string);
                    string paymentIntentID = intent_serialized.id;

                    //create a confirm payment and push the payment to stripe.com
                    ProcessPayment confirm = new ProcessPayment();
                    var confirm_value = confirm.ConfirmPayment(paymentMethodID, paymentIntentID);
                    var confirm_string = confirm_value.Result;
                    PaymentMethods confirm_serialized = JsonConvert.DeserializeObject<PaymentMethods>(confirm_string);

                    //updates the Tuition amount curer
                    Pay.TuitionBalance = CurrentAccount.Tuition - Convert.ToDouble(Pay.totalPayment);
                    Pay.UserInfoID = CurrentAccount.ID;
                    CurrentAccount.Tuition = Pay.TuitionBalance;

                    _context.Payments.Add(Pay);
                    await _context.SaveChangesAsync();
                }
            }

            return Redirect("./PaymentStatus");

        }
    }

    public class ProcessPayment
    {
        readonly string key = "sk_test_51J32ofLSFb2y1AXkR95ktJfpLghTgwwCbxAds9uMjbChXPT0TVokQSGgXdDPTQONhNNqkoIxFt9ZWfVsPoEljKtb00QzsRLM9o";

        //method that creates the payment method on stripe.com using HttpClient POST request
        public async Task<string> CreatePaymentMethod(string cardNumber, string exMonth, string exYear)
        {
            string url = "https://api.stripe.com/v1/payment_methods";



            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);


            string body = "type=card&card[number]=" + cardNumber + "&card[exp_month]=" + exMonth + "&card[exp_year]=" + exYear;

            var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        //method that creates a payment intent for the amount specified on the payment form
        public async Task<string> CreatePaymentIntent(string email, string paymentAmount)
        {
            string url = "https://api.stripe.com/v1/payment_intents";


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            //creates the body of the POST request that contains elements of the Payment Intent for the customer
            //hard-coded and will need to be replaced with session data
            string body = "amount=" + paymentAmount + "&currency=usd&description=Payment%20for%3A%20" + email;

            var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        //method that links the payment method to the payment intent and confirms the payment has been processed
        public async Task<string> ConfirmPayment(string paymentMethodID, string paymentIntentID)
        {
            string url = "https://api.stripe.com/v1/payment_intents/" + paymentIntentID + "/confirm";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            //creates body of POST request with required payment method
            string body = "payment_method=" + paymentMethodID;

            var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded"));
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

    }

    //inner class used to map the deserialized json object from the api to serialized data
    public class PaymentMethods
    {
        public string id { get; set; }

        public string client_secret { get; set; }

        public string status { get; set; }
    }
}
