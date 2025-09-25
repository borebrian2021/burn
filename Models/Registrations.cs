namespace BURN_SOCIETY.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Registrations
    {
            [Key]
            public int Id { get; set; } // "Yes" or "No"
            public string Attend { get; set; } // "Yes" or "No"
            public string FName { get; set; }
            public string Status_code { get; set; }
            public string SName { get; set; }
            public string Cadre { get; set; }
            public string TelephoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string Institution { get; set; }
        public string PaymentConfirmation { get; set; }// "Yes" or "No"
        public string Payment_Status { get; set; }// Corresponds to <textarea name="conclusion">
        public float Ammount { get; set; } = 0;// Corresponds to <textarea name="conclusion">
        public string ReffCode { get; set; } // Corresponds to <textarea name="conclusion">
        public string PaymentCategory { get; set; } // Corresponds to <textarea name="conclusion">
        public string Currency { get; set; } // Corresponds to <textarea name="conclusion">
        public bool KenyanNurse { get; set; } // Corresponds to <textarea name="conclusion">
        public bool Refference { get; set; } 
    }
    
}
