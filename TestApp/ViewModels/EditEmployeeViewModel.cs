using System.Collections.Generic;
using TestApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace TestApp.ViewModels
{
    public class EditEmployeeViewModel
    {
        public IList<string> EmployeeRoles { get; set; }
        
        public IList<string> EmployeePositions { get; set; }
        
        public Employee Employee { get; set; }
        
        public List<IdentityRole> Roles { get; set; }
        
        public List<string> Positions { get; set; }
    }
}