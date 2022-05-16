using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestApp.Models.Entities;

namespace TestApp.Models
{
    public static class UserManagerExtansions
    {
        public async static Task<bool> UpdateRolesAsync(this UserManager<Employee> userManager, Employee employee, IList<string> selectRoles)
        {
            var userRoles = await userManager.GetRolesAsync(employee);
            var addedRoles = selectRoles.Except(userRoles);
            var removeRoles = userRoles.Except(selectRoles);

            var result = await userManager.AddToRolesAsync(employee, addedRoles);
            var resultRemove = await userManager.RemoveFromRolesAsync(employee, removeRoles);

            if (resultRemove.Succeeded)
                return true;
            return false;
        }
    }
}