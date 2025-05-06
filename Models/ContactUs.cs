using System.ComponentModel.DataAnnotations;

namespace BurnSociety.umbraco.custome_models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public  string FullNames { get; set; }
        public required string Email { get; set; }
        public  string Subject { get; set; }
        public  string Number { get; set; }
        public required string Message { get; set; }
        public string Date { get; set; } = "User";
       

    }
}
