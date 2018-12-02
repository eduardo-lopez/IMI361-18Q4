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
    public class OrganizacionesController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public OrganizacionesController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/Organizaciones
        [HttpGet]
        public IEnumerable<OrganizacionMSTR> GetOrganizacion()
        {
            return _context.OrganizacionMSTR;
        }

        // GET: api/Organizaciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizacionMSTR = await _context.OrganizacionMSTR.FindAsync(id);

            if (organizacionMSTR == null)
            {
                return NotFound();
            }

            return Ok(organizacionMSTR);
        }

        // PUT: api/Organizaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizacion([FromRoute] int id, [FromBody] OrganizacionMSTR organizacionMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizacionMSTR.OrganizacionID)
            {
                return BadRequest();
            }

            _context.Entry(organizacionMSTR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizacionExists(id))
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

        // POST: api/Organizaciones
        [HttpPost]
        public async Task<IActionResult> PostOrganizacion([FromBody] OrganizacionMSTR organizacionMSTR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrganizacionMSTR.Add(organizacionMSTR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizacion", new { id = organizacionMSTR.OrganizacionID }, organizacionMSTR);
        }

        // DELETE: api/Organizaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizacionMSTR = await _context.OrganizacionMSTR.FindAsync(id);
            if (organizacionMSTR == null)
            {
                return NotFound();
            }

            _context.OrganizacionMSTR.Remove(organizacionMSTR);
            await _context.SaveChangesAsync();

            return Ok(organizacionMSTR);
        }

        private bool OrganizacionExists(int id)
        {
            return _context.OrganizacionMSTR.Any(e => e.OrganizacionID == id);
        }
    }
}