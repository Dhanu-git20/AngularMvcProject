using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Patient
    {
        [Key]
        public int id { get; set; }

        [Required]
        [RegularExpression("^[a-z]{1,10}$")]
        public string name { get; set; }

       // public string problem { get; set; }
       // [JsonIgnore]
        //[IgnoreDataMember] 
        public List<Problem> problems { get; set; }

    }
    public class Problem
   { 
        public int id { get; set; }
        public string problem { get; set; }
        
        public Patient patient { get; set; }
    }


}
