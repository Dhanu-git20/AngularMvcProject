using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Dal
{
    public class PatientDal : DbContext
    {
        string ConnStr= "";

        public PatientDal(string constr)
        {
            this.ConnStr = constr;
        }
        //Constructor
        public PatientDal(DbContextOptions<PatientDal> options) : base(options)
        {

        }
        public PatientDal()
        {

        }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-M189A6Q;Initial Catalog=PatientDB;Integrated Security=True");
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Patient>().HasKey(p => p.id);

            // modelBuilder.Entity<Problem>().HasKey(p => p.id);
         

            modelBuilder.Entity<Patient>().Property(t => t.id).ValueGeneratedNever();
          

            modelBuilder.Entity<Patient>()
                .ToTable("tblPatient");

            modelBuilder.Entity<Problem>()
                .ToTable("tblProblem");


            //1 to many
            modelBuilder.Entity<Patient>()
               .HasMany(c => c.problems)
               .WithOne(e => e.patient);
            modelBuilder.Entity<Patient>();

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Problem> Problems { get; set; }
      




    }
}
