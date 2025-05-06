using System.ComponentModel.DataAnnotations;

namespace BurnSociety.umbraco.custome_models
{
    public class Files
    {
        [Key]
        public int Id { get; set; }
        public  string Email { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Institution { get; set; }
        public required string Abstract { get; set; }
        public  string Phone { get; set; }
       


    }
}
