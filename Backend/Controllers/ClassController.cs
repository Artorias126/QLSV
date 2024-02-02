using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV.Entities;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly StudentContext _context;

        public ClassController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetClasses()
        {
            var classes = _context.Lops.ToList();
            return classes;
        }

        [HttpGet("{id}")]
        public ActionResult<Class> GetClass(string id)
        {
            var classObj = _context.Lops.Find(id);

            if (classObj == null)
            {
                return NotFound();
            }

            return classObj;
        }

        [HttpPut("{id}")]
        public IActionResult PutClass(string id, Class classObj)
        {
            if (id != classObj.ClassID)
            {
                return BadRequest("Class ID mismatch");
            }

            var existingClass = _context.Lops.Find(id);

            if (existingClass == null)
            {
                return NotFound();
            }

            // Check if the updated ClassID conflicts with an existing one
            if (id != classObj.ClassID && ClassExists(classObj.ClassID))
            {
                return Conflict("Class with the same ID already exists.");
            }

            _context.Entry(existingClass).CurrentValues.SetValues(classObj);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpPost]
        public ActionResult<Class> PostClass(Class classObj)
        {
            if (ClassExists(classObj.ClassID))
            {
                return Conflict("Class with the same ID already exists.");
            }

            _context.Lops.Add(classObj);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetClass), new { id = classObj.ClassID }, classObj);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClass(string id)
        {
            var classObj = _context.Lops.Find(id);
            if (classObj == null)
            {
                return NotFound();
            }

            _context.Lops.Remove(classObj);
            _context.SaveChanges();

            return NoContent();
        }


        private bool ClassExists(string id)
        {
            return _context.Lops?.Any(e => e.ClassID == id) ?? false;
        }
    }
}
