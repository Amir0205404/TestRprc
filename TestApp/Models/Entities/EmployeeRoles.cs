using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TestApp.Models.Entities
{
    public class EmployeeRoles : IdentityUserRole<string>
    {
        public override string UserId { get; set; }
        
        public override string RoleId { get; set; }
    }
}