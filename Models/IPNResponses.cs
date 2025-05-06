namespace BURN_SOCIETY.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IPNResponses
    {
        [Key]
        public int Id { get; set; }
        public string OrderTrackingId { get; set; }
        public string OrderNotificationType { get; set; }
        public string OrderMerchantReference { get; set; }
       
    }
}
