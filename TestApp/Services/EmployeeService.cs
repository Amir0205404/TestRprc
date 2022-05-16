using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using TestApp.Models.Entities;

namespace TestApp.Services
{
    public class EmployeeService
    {
        private EFContext db;

        public EmployeeService(EFContext context)
        {
            db = context;
        }

        public void AddToPositions(Employee employee, IEnumerable<string> positionsName)
        {
            foreach (var positionName in positionsName)
            {
                db.employee_positions.Add(new EmployeePositions()
                {
                    employee_id = employee.Id,
                    position_id = db.positions.FirstOrDefault(p => p.position_name == positionName).position_id
                });
            }

            db.SaveChanges();
        }

        public List<string> GetPositions(Employee employee)
        {
            return db.employee_positions
                .Where(e => e.employee_id == employee.Id)
                .Include(p => p.Position)
                .Select(pn => pn.Position.position_name).ToList();
        }

        public void UpdateToPositions(Employee employee, List<string> selected_positions)
        {
            var userRoles = GetPositions(employee);
            var addedRoles = selected_positions.Except(userRoles);
            var removeRoles = userRoles.Except(selected_positions);

            AddToPositions(employee, addedRoles);
            RemoveToPositions(employee, removeRoles);
        }

        public void RemoveToPositions(Employee employee, IEnumerable<string> positions)
        {
            var removeEmployeePositions =
                db.employee_positions
                    .Include(p => p.Position)
                    .Where(e_p => e_p.employee_id == employee.Id && positions.Contains(e_p.Position.position_name));
            
            db.employee_positions.RemoveRange(removeEmployeePositions);
            db.SaveChanges();
        }
    }
}