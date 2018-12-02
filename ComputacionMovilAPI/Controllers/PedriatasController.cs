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
    public class PedriatasController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public PedriatasController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedriatas
        [HttpGet]
        public IEnumerable<PedriataMSTR> GetPedriata()
        {
            return _context.PedriataMSTR;
        }

        // GET: api/Pedriatas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedriata([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedriataMSTR = await _context.PedriataMSTR.FindAsync(id);

            if (pedriataMSTR == null)
            {
                return NotFound();
            }

            return Ok(pedriataMSTR);
        }

        // PUT: api/Pedriatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedriata([FromRoute] int id, [FromBody] PedriataMSTR pedriataMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedriataMSTR.PedriataID)
            {
                return BadRequest();
            }

            _context.Entry(pedriataMSTR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedriataExists(id))
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

        // POST: api/Pedriatas
        [HttpPost]
        public async Task<IActionResult> PostPedriata([FromBody] PedriataMSTR pedriataMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PedriataMSTR.Add(pedriataMSTR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedriata", new { id = pedriataMSTR.PedriataID }, pedriataMSTR);
        }

        // DELETE: api/Pedriatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedriata([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedriataMSTR = await _context.PedriataMSTR.FindAsync(id);
            if (pedriataMSTR == null)
            {
                return NotFound();
            }

            _context.PedriataMSTR.Remove(pedriataMSTR);
            await _context.SaveChangesAsync();

            return Ok(pedriataMSTR);
        }

        private bool PedriataExists(int id)
        {
            return _context.PedriataMSTR.Any(e => e.PedriataID == id);
        }
    }
}