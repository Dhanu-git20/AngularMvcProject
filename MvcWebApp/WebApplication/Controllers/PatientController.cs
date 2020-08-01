using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Dal;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

//namespace WebApplication.Controllers
//{
        //[EnableCors("AllowOriginRule")]
       // public class PatientController : Controller
       // {
           // string constr = "";
           // public PatientController(IConfiguration configuration)
           // {
               // constr = configuration["ConnStr"];
            //}
            //public IActionResult Submit([FromBody] Patient obj)
           // {
                //var context = new ValidationContext(obj, null, null);
                //var result = new List<ValidationResult>();
                //var isValid = Validator.TryValidateObject(obj, context, result, true);

                //if (result.Count == 0)
                //{
                   // PatientDal dal = new PatientDal(constr);
                    //dal.Database.EnsureCreated();
                    //dal.Add(obj);// Save in Memory
                    //dal.SaveChanges();//physical commit
                    //List<Patient> recs = dal.Patients.ToList<Patient>();

                   // return Ok(recs);
                //}
                //else
              //  {
                   // return StatusCode(500, result);
                //}
            //}
            //public IActionResult Search(string patientName)
            //{
                //PatientDal dal = new PatientDal(constr);
                //List<Patient> search = (from temp in dal.Patients
                                            // where temp.name == patientName
                                            // select temp)
                                            //.ToList<Patient>();
              //  return Ok(search);
           // }


        //}
    
//}
