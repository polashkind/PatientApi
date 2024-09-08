using Microsoft.EntityFrameworkCore;
using PatientApi.Models;

namespace PatientApi.Data
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .OwnsOne(p => p.Name, n =>
                {
                    n.Property(name => name.Given).HasColumnName("Given");
                    n.Property(name => name.Family).HasColumnName("Family");
                });
        }
    }
}
