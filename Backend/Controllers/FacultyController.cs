// ... (namespace and using statements)

using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using QLSV.Entities;
using System.Numerics;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly StudentContext _context;

        public FacultyController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            var faculties = await _context.Khoas.ToListAsync();
            if (faculties == null || faculties.Count == 0)
            {
                return NotFound();
            }
            return Ok(faculties); // Return the list of faculties
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(string id)
        {
            var faculty = await _context.Khoas.FindAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found.");
            }
            return Ok(faculty);
        }
        [HttpPost]
        public async Task<ActionResult<Faculty>> CreateFaculty(Faculty faculty)
        {
            _context.Khoas.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaculty", new { id = faculty.FacultyID }, faculty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaculty(string id, Faculty faculty)
        {
            if (id != faculty.FacultyID)
            {
                return BadRequest("ID mismatch.");
            }

            var existingFaculty = await _context.Khoas.FindAsync(id);
            if (existingFaculty == null)
            {
                return NotFound("Faculty not found.");
            }

            // Update properties of existingFaculty
            existingFaculty.FacultyName = faculty.FacultyName;

            // Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts
                // ...
            }

            // Update referencing Assessment records
            var referencingAssessments = await _context.Bangtongkets
                .Where(a => a.FacultyName == id)
                .ToListAsync();

            foreach (var assessment in referencingAssessments)
            {
                assessment.FacultyName = faculty.FacultyName;
            }

            // Save changes again
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflicts
                // ...
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(string id)
        {
            var faculty = await _context.Khoas.FindAsync(id);
            if (faculty == null)
            {
                return NotFound("Faculty not found.");
            }

            _context.Khoas.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultyExists(string id)
        {
            return _context.Khoas.Any(e => e.FacultyID == id);
        }
    }
}





