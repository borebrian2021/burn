using System.ComponentModel.DataAnnotations;

namespace BurnSociety.umbraco.custome_models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string FullNames { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public required string Password { get; set; }
        public string role { get; set; } = "User";
        public Boolean IsConfirmed { get; set; } = false;
        public Boolean rememberPassword { get; set; } = false;


    }
}
