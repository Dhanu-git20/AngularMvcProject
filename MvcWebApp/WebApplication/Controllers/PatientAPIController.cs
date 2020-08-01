using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApplication.Dal;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAPIController : ControllerBase
    {
        string constr = "";
        //PatientDal dal = null;
        public PatientAPIController()//IConfiguration configuration)
        {
          ///  constr = configuration["ConnStr"];
            // dal = _dal;
        }
        // GET: api/<PatientAPIController>
       // [HttpGet]
        // public IEnumerable<string> Get()
        // {
         //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public string Get()//Edit data
        {
           PatientDal dal = new PatientDal();
            List<Patient> patcoll = dal.Patients.
                Include(pat => pat.problems)
                    .ToList();
            var json = JsonConvert.SerializeObject(patcoll, Formatting.None,
                                                    new JsonSerializerSettings()
                                                    {
                                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                    });
            return json;

        }

        //GET api/<PatientAPIController>/5
        //[HttpGet]
        //public IActionResult Get(string patientName)//search data
        //{
        //    PatientDal dal = new PatientDal(constr);
        //    List<Patient> search = (from temp in dal.Patients
        //                            where temp.name == patientName
        //                            select temp)
        //                               .ToList<Patient>();
        //    return Ok(search);
        //}


        // POST api/<PatientAPIController>
        [HttpPost]
        public IActionResult Post([FromBody] Patient obj)//Insert Data

        {
            var context = new ValidationContext(obj, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, result, true);

            if (result.Count == 0)
            {
                PatientDal dal = new PatientDal();
                dal.Database.EnsureCreated();
                dal.Add(obj);// Save in 

                dal.SaveChanges();//physical commit
                List<Patient> recs = dal.Patients.
                    Include(pat => pat.problems)
                    .ToList<Patient>();

                return Ok(recs);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        // PUT api/<SecurityController>/5
        [HttpPut]
        public string Put(Patient obj)//Update data
        {
            PatientDal dal = new PatientDal(constr);
            var patData = dal.Patients.Where(p => p.id == obj.id).FirstOrDefault();
           // var probData = dal.Problems.Where(p => p.id == obj.id).FirstOrDefault();
            if (patData != null)
            {
                patData.name = obj.name;
               patData.problems = obj.problems;
                dal.Patients.Attach(patData);
                dal.Entry(patData).Property(a => a.name).IsModified = true;
               dal.Entry(patData).Collection(a => a.problems).IsModified = true;
                dal.SaveChanges();
            }

            var patcoll = dal.Patients.Include(p => p.problems).ToList();
            var json = JsonConvert.SerializeObject(patcoll, Formatting.None,
                                        new JsonSerializerSettings()
                                        {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                        });
            return json;
        }
         // DELETE api/<PatientAPIController>/5
        [HttpDelete("{patientId}")]
        public IActionResult Delete(int patientId)
        {
            PatientDal dal = new PatientDal();
            var patient = dal.Patients.Include(p => p.problems).SingleOrDefault(p => p.id == patientId);
 
            foreach (var prb in patient.problems.ToList())
                dal.Problems.Remove(prb);
            dal.Patients.Remove(patient);

            dal.SaveChanges();


            List<Patient> patientcoll = dal.Patients.Include(pat => pat.problems)
                    .ToList();
            return Ok(patientcoll);

        }
           
    }
  
}
