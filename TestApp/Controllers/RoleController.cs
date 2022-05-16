using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestApp.Models.Entities;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return View(_roleManager.Roles.ToList());
        }
            
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAllRoles");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {   
                return View(role);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(IdentityRole role)
        {
            var updateRole = await _roleManager.FindByIdAsync(role.Id);
            if (updateRole != null)
            {
                updateRole.Name = role.Name;
                var result = await _roleManager.UpdateAsync(updateRole);
                
                if (result.Succeeded)
                    return RedirectToAction("GetAllRoles");
            }

            return NotFound();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }
            }

            return NotFound();
        }
    }
}