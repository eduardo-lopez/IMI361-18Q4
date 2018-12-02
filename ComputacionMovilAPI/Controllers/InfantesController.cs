using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputacionMovilAPI.Models;

namespace ComputacionMovilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfantesController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public InfantesController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/Infantes
        [HttpGet]
        public IEnumerable<InfanteMSTR> GetInfante()
        {
            return _context.InfanteMSTR;
        }

        // GET: api/Infantes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfante([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infanteMSTR = await _context.InfanteMSTR.FindAsync(id);

            if (infanteMSTR == null)
            {
                return NotFound();
            }

            return Ok(infanteMSTR);
        }

        // PUT: api/Infantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfante([FromRoute] int id, [FromBody] InfanteMSTR infanteMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infanteMSTR.InfanteID)
            {
                return BadRequest();
            }

            _context.Entry(infanteMSTR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfanteExists(id))
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

        // POST: api/Infantes
        [HttpPost]
        public async Task<IActionResult> PostInfante([FromBody] InfanteMSTR infanteMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InfanteMSTR.Add(infanteMSTR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfante", new { id = infanteMSTR.InfanteID }, infanteMSTR);
        }

        // DELETE: api/Infantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfante([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infanteMSTR = await _context.InfanteMSTR.FindAsync(id);
            if (infanteMSTR == null)
            {
                return NotFound();
            }

            _context.InfanteMSTR.Remove(infanteMSTR);
            await _context.SaveChangesAsync();

            return Ok(infanteMSTR);
        }

        private bool InfanteExists(int id)
        {
            return _context.InfanteMSTR.Any(e => e.InfanteID == id);
        }
    }
}