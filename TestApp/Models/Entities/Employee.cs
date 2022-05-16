using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TestApp.Models.Entities
{
    public class Employee : IdentityUser
    {
        public DateTime start_date { get; set; } = DateTime.Now;
        
        public DateTime? end_date { get; set; } = DateTime.Now;

        public int act_status { get; set; } = 1;

        public string first_name { get; set; } = "Газимагомед";

        public string second_name { get; set; } = "A.K.";

        public string last_name { get; set; } = "гумбетовский отморозок";
        
       
        public List<EmployeePositions> EmployeePositions { get; set; }
    }
}