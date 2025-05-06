using BURN_SOCIETY.Models;
using BurnSociety.umbraco.custome_models;
using BURN_SOCIETY.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BurnSociety.Application
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Register> Register { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<IPNResponses> IPNResponses { get; set; }
        public DbSet<Registrations> Registrations { get; set; }
        public DbSet<PaymentResponse> PaymentResponse { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
                : base(options)
        {
        }
    }

}

