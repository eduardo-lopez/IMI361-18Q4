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
    public class HitosController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public HitosController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/Hitos
        [HttpGet]
        public IEnumerable<HitoMSTR> GetHito()
        {
            return _context.HitoMSTR;
        }

        // GET: api/Hitos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHito([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hitoMSTR = await _context.HitoMSTR.FindAsync(id);

            if (hitoMSTR == null)
            {
                return NotFound();
            }

            return Ok(hitoMSTR);
        }

        // PUT: api/Hitos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHito([FromRoute] int id, [FromBody] HitoMSTR hitoMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hitoMSTR.HitoID)
            {
                return BadRequest();
            }

            _context.Entry(hitoMSTR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HitoExists(id))
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

        // POST: api/Hitos
        [HttpPost]
        public async Task<IActionResult> PostHito([FromBody] HitoMSTR hitoMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HitoMSTR.Add(hitoMSTR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHito", new { id = hitoMSTR.HitoID }, hitoMSTR);
        }

        // DELETE: api/Hitos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHito([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hitoMSTR = await _context.HitoMSTR.FindAsync(id);
            if (hitoMSTR == null)
            {
                return NotFound();
            }

            _context.HitoMSTR.Remove(hitoMSTR);
            await _context.SaveChangesAsync();

            return Ok(hitoMSTR);
        }

        private bool HitoExists(int id)
        {
            return _context.HitoMSTR.Any(e => e.HitoID == id);
        }
    }
}