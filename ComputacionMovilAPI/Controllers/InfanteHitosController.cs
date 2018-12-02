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
    public class InfanteHitosController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public InfanteHitosController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/InfanteHitos
        [HttpGet]
        public IEnumerable<InfanteHitoXREF> GetInfanteHitoXREF()
        {
            return _context.InfanteHitoXREF;
        }

        // GET: api/InfanteHitos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfanteHitoXREF([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infanteHitoXREF = await _context.InfanteHitoXREF.FindAsync(id);

            if (infanteHitoXREF == null)
            {
                return NotFound();
            }

            return Ok(infanteHitoXREF);
        }

        // PUT: api/InfanteHitos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfanteHitoXREF([FromRoute] int id, [FromBody] InfanteHitoXREF infanteHitoXREF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infanteHitoXREF.HitoID)
            {
                return BadRequest();
            }

            _context.Entry(infanteHitoXREF).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfanteHitoXREFExists(id))
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

        // POST: api/InfanteHitos
        [HttpPost]
        public async Task<IActionResult> PostInfanteHitoXREF([FromBody] InfanteHitoXREF infanteHitoXREF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InfanteHitoXREF.Add(infanteHitoXREF);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InfanteHitoXREFExists(infanteHitoXREF.HitoID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInfanteHitoXREF", new { id = infanteHitoXREF.HitoID }, infanteHitoXREF);
        }

        // DELETE: api/InfanteHitos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfanteHitoXREF([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infanteHitoXREF = await _context.InfanteHitoXREF.FindAsync(id);
            if (infanteHitoXREF == null)
            {
                return NotFound();
            }

            _context.InfanteHitoXREF.Remove(infanteHitoXREF);
            await _context.SaveChangesAsync();

            return Ok(infanteHitoXREF);
        }

        private bool InfanteHitoXREFExists(int id)
        {
            return _context.InfanteHitoXREF.Any(e => e.HitoID == id);
        }
    }
}