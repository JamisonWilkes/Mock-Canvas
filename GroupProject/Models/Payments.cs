using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationHW1.Models
{
    public class Payments
    {   
        [Key]
        public int paymentID { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [StringLength(16, ErrorMessage = "Card Number not valid")]
        [Display(Name = "Card Number")]
        public string cardNumber { get; set; }

        [Required]
        [Display(Name = "Card Holder")]
        public string nameOnCard { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM}")]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime cardExpiration { get; set; }

        [Required]
        [Display(Name = "CVC number")]
        public string cvcNumber { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        public string totalPayment { get; set; }

        public double TuitionBalance { get; set; }

        public int UserInfoID { get; set; }

        public UserInfo UserInfo { get; set; }

        //public Payments()
        //{
        //    totalTuition = 100;
        //}
    }
}
