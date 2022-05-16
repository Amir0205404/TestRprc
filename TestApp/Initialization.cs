using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestApp.Models;
using TestApp.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestApp
{
    public class Initialization
    {
        public static async Task RoleAndAccountInitialize(UserManager<Employee> userManager,  RoleManager<IdentityRole> roleManager, EFContext context)
        {
            string login = "AmroSuper";
            string password = Environment.GetEnvironmentVariable("password", EnvironmentVariableTarget.User);
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }
            if (context.positions.FirstOrDefault(p => p.position_name == "Сисадмин") == null)
            {
                context.positions.Add(new Position() { position_name = "Сисадмин"});
                await context.SaveChangesAsync();
            }
            if (await userManager.FindByNameAsync(login) == null)
            {
                Employee employee = new Employee() { UserName = login };
                var saveEmoloyee  = await userManager.CreateAsync(employee, password);
                if (saveEmoloyee.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, "admin");
                    context.employee_positions.Add(new EmployeePositions()
                    {
                        employee_id = employee.Id,
                        position_id = context.positions.FirstOrDefault(n => n.position_name == "Сисадмин").position_id
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}