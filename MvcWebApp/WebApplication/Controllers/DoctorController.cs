using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.Dal;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
       string constr = "";
        //PatientDal dal = null;
        public DoctorController()//IConfiguration configuration)
        {
            ///  constr = configuration["ConnStr"];
            // dal = _dal;
        }

        // GET: api/<DoctorController>


        // GET api/<DoctorController>/5
        [HttpGet]
        public string Get()//Edit data
        {
            DoctorDal dal = new DoctorDal();
            List<DoctorModel> doccoll = dal.Doctors.ToList<DoctorModel>();
         
            var json = JsonConvert.SerializeObject(doccoll, Formatting.None,
                                                    new JsonSerializerSettings()
                                                    {
                                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                    });
            return json;

        }


        // POST api/<DoctorController>
        [HttpPost]
        public IActionResult Post([FromBody] DoctorModel obj)//Insert Data

        {
            var context = new ValidationContext(obj, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, result, true);

            if (result.Count == 0)
            {
                DoctorDal dal = new DoctorDal();
                dal.Database.EnsureCreated();
                dal.Add(obj);// Save in 

                dal.SaveChanges();//physical commit
                List<DoctorModel> recs = dal.Doctors.ToList <DoctorModel>();

                return Ok(recs);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
