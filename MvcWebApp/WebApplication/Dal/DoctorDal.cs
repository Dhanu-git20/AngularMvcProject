using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Dal
{
    public class DoctorDal : DbContext
    {
        string ConnStr = "";

        public DoctorDal(string constr)
        {
            this.ConnStr = constr;
        }
        //Constructor
        public DoctorDal(DbContextOptions<PatientDal> options) : base(options)
        {

        }
        public DoctorDal()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-M189A6Q;Initial Catalog=PatientDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorModel>().HasKey(p => p.id);


            modelBuilder.Entity<DoctorModel>()
                .ToTable("tblDoctor");

        }
        public DbSet<DoctorModel> Doctors { get; set; }
    }
}

