using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Models.Entities;

namespace TestApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class PositionController : Controller
    {
        private EFContext db;
        public PositionController(EFContext context)
        {
            db = context;
        }
        
        [HttpGet]
        public IActionResult GetAllPositions()
        {
            return View(db.positions.ToList());
        }

        [HttpGet]
        public IActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPosition(Position position)
        {
            if (ModelState.IsValid)
            {
                db.positions.Add(position);
                db.SaveChanges();
                
                return RedirectToAction("GetAllPositions");

            }
            
            return BadRequest();
        }
        
        [HttpGet]
        public IActionResult UpdatePosition(Guid positionId)
        {
            Position position = db.positions.FirstOrDefault(p => p.position_id == positionId);
            
            if (position != null)
            {
                return View(position);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePosition(Position position)
        {
            if (ModelState.IsValid)
            {
                db.positions.Update(position);
                db.SaveChanges();

                return RedirectToAction("GetAllPositions");
            }

            return BadRequest();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePosition(Guid positionId)
        {
            Position position = db.positions.FirstOrDefault(p => p.position_id == positionId);
            if (position != null)
            {
                db.Remove(position);
                db.SaveChanges();
                return RedirectToAction("GetAllPositions");
            }

            return BadRequest();
        }
    }
}