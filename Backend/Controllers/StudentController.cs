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
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Sinhviens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetSinhviens()
        {
          if (_context.Sinhviens == null)
          {
              return NotFound();
          }
            return await _context.Sinhviens.ToListAsync();
        }

        // GET: api/Sinhviens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetSinhvien(int id)
        {
          if (_context.Sinhviens == null)
          {
              return NotFound();
          }
            var sinhvien = await _context.Sinhviens.FindAsync(id);

            if (sinhvien == null)
            {
                return NotFound();
            }

            return sinhvien;
        }

        // PUT: api/Sinhviens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhvien(int id, Student sinhvien)
        {
            if (id != sinhvien.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(sinhvien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhvienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sinhviens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostSinhvien(Student sinhvien)
        {
          if (_context.Sinhviens == null)
          {
              return Problem("Entity set 'QLSVContext.Sinhviens'  is null.");
          }
            _context.Sinhviens.Add(sinhvien);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SinhvienExists(sinhvien.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSinhvien", new { id = sinhvien.StudentID }, sinhvien);
        }

        // DELETE: api/Sinhviens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhvien(int id)
        {
            if (_context.Sinhviens == null)
            {
                return NotFound();
            }
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            _context.Sinhviens.Remove(sinhvien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SinhvienExists(int id)
        {
            return (_context.Sinhviens?.Any(e => e.StudentID == id)).GetValueOrDefault();
        }
    }
}
