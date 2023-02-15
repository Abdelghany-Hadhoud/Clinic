using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public bool IsTesting { get; set; }
        public DbContextClass(IConfiguration Configuration, DbContextOptions options, bool isTesting = false) : base(options)
        {
            this.Configuration = Configuration;
            this.IsTesting = isTesting;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (this.IsTesting == false)
                options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
