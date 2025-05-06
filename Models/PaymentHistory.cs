using BURN_SOCIETY.Models;

namespace BURN_SOCIETY.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class PaymentResponse
    {
        [Key]
        public int Id { get; set; }
        public string payment_method { get; set; }
        public double amount { get; set; }
        public DateTime created_date { get; set; }
        public string confirmation_code { get; set; }
        public string order_tracking_id { get; set; }
        public string payment_status_description { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public string payment_account { get; set; }
        public string call_back_url { get; set; }
        public int status_code { get; set; }
        public string merchant_reference { get; set; }
        public string account_number { get; set; }
        public string payment_status_code { get; set; }
        public string currency { get; set; }
        public string error_type { get; set; }
        public string code { get; set; }
        public string status { get; set; }

    }
}

  