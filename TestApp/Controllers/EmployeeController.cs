using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Models.Entities;
using TestApp.Models.DTO;
using TestApp.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestApp.Services;

namespace TestApp.Controllers
{
    [Authorize(Roles = "admin")]   
    public class EmployeeController : Controller
    {
        private EFContext db;
        private UserManager<Employee> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private EmployeeService _employeeService;
        public EmployeeController(EFContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, EmployeeService employeeService)
        {
            this.db = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            List<EmployeeInfoDto> employeesList = new List<EmployeeInfoDto>();
            
            foreach (var employee in _userManager.Users.ToList())
            {
                employeesList.Add(new EmployeeInfoDto()
                {
                    EmployeeLogin = employee.UserName,
                    FirstName = employee.first_name,
                    SecondName = employee.second_name,
                    LastName = employee.last_name,
                    RoleNameList = await _userManager.GetRolesAsync(employee),
                    PositionNameList = _employeeService.GetPositions(employee)
                });
            }

            return View("EmployeeList", employeesList);
        }
        
        [HttpGet]
        public IActionResult AddEmployee()
        {
            RegisterViewModel model = new RegisterViewModel()
            {
                RoleNameList = _roleManager.Roles.Select(r => r.Name).ToList(),
                PositionNameList = db.positions.Select(p => p.position_name).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(RegisterViewModel model, List<string> roleNames, List<string> positionNames)
        {
            if (ModelState.IsValid)
            {  
                Employee emp = new Employee() { UserName = model.Login };
                var resultRegistr = await _userManager.CreateAsync(emp, model.Password);
                if (resultRegistr.Succeeded)
                {
                    await _userManager.AddToRolesAsync(emp, roleNames);
                    _employeeService.AddToPositions(emp, positionNames);
                    
                    return RedirectToAction("GetAllEmployees", "Employee");
                }
                else
                {
                    foreach (var error in resultRegistr.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            model.RoleNameList = _roleManager.Roles.Select(r => r.Name).ToList();
            model.PositionNameList = db.positions.Select(p => p.position_name).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string EmployeeLogin)
        {   
            Employee employee = await _userManager.FindByNameAsync(EmployeeLogin);
            if (employee != null)
            {
                EditEmployeeViewModel model = new EditEmployeeViewModel()
                {
                    Employee = employee,
                    EmployeeRoles = await _userManager.GetRolesAsync(employee),
                    EmployeePositions = _employeeService.GetPositions(employee),
                    Roles = _roleManager.Roles.ToList(),
                    Positions = db.positions.Select(p => p.position_name).ToList()
                };
                return View(model);
            }
            else
                return NotFound();
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeViewModel model, List<string> selected_roles, List<string> selected_positions)
        {
            Employee employee = await _userManager.FindByIdAsync(model.Employee.Id);
            if (employee != null)
            {
                employee.UserName = model.Employee.UserName;
                employee.first_name = model.Employee.first_name;
                employee.second_name = model.Employee.second_name;
                employee.last_name = model.Employee.last_name;
                
                await _userManager.UpdateRolesAsync(employee, selected_roles);
                _employeeService.UpdateToPositions(employee, selected_positions);
                
                var resultUpdate = await _userManager.UpdateAsync(employee);
                
                
                if (resultUpdate.Succeeded)
                {
                    return RedirectToAction("GetAllEmployees");
                }
            }
            return BadRequest();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(string employeeLogin)
        {
            var employee = await _userManager.FindByNameAsync(employeeLogin);
            if (employee != null)
            {   
                await _userManager.DeleteAsync(employee);
            }

            return RedirectToAction("GetAllEmployees");
        }
    }
}