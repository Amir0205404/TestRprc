using System.Collections.Generic;

namespace TestApp.Models.DTO
{
    public class EmployeeInfoDto
    {
        public string EmployeeLogin { get; set; }
        
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }
        
        public IList<string> RoleNameList { get; set; }
        
        public IList<string> PositionNameList { get; set; } 
    }
}