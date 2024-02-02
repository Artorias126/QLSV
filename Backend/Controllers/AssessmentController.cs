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
    public class AssessmentController : ControllerBase
    {
        private readonly StudentContext _context;

        public AssessmentController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Bangtongkets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetBangtongkets()
        {
          if (_context.Bangtongkets == null)
          {
              return NotFound();
          }
            return await _context.Bangtongkets.ToListAsync();
        }

        // GET: api/Bangtongkets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetBangtongket(int id)
        {
          if (_context.Bangtongkets == null)
          {
              return NotFound();
          }
            var bangtongket = await _context.Bangtongkets.FindAsync(id);

            if (bangtongket == null)
            {
                return NotFound();
            }

            return bangtongket;
        }

        // PUT: api/Bangtongkets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBangtongket(int id, Assessment bangtongket)
        {
            if (id != bangtongket.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(bangtongket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BangtongketExists(id))
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

        // POST: api/Bangtongkets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assessment>> PostBangtongket(Assessment bangtongket)
        {
          if (_context.Bangtongkets == null)
          {
              return Problem("Entity set 'QLSVContext.Bangtongkets'  is null.");
          }
            _context.Bangtongkets.Add(bangtongket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BangtongketExists(bangtongket.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBangtongket", new { id = bangtongket.StudentID }, bangtongket);
        }

        // DELETE: api/Bangtongkets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBangtongket(int id)
        {
            if (_context.Bangtongkets == null)
            {
                return NotFound();
            }
            var bangtongket = await _context.Bangtongkets.FindAsync(id);
            if (bangtongket == null)
            {
                return NotFound();
            }

            _context.Bangtongkets.Remove(bangtongket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BangtongketExists(int id)
        {
            return (_context.Bangtongkets?.Any(e => e.StudentID == id)).GetValueOrDefault();
        }
    }
}
